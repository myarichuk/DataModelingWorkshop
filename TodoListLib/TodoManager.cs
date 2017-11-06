using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Documents.Queries;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace TodoListLib
{
    public class TodoManager : IDisposable
    {
        private readonly IDocumentStore _store;

        public TodoManager(string url, string database)
        {
            _store = new DocumentStore
            {
                Urls = new[] { url },
                Database = database
            };

            _store.Initialize();
            CreateDatabaseIfNotExists(database);
        }

        public string Add(TodoItem item)
        {
            using (var session = _store.OpenSession())
            {
                if (item.Id == null || 
                    !session.Advanced.Exists(item.Id))
                {
                    item.Started = DateTime.UtcNow;
                }
                
                //tasks usually do not have seconds/minutes/hours in their deadline, so...
                item.Deadline = item.Deadline.Date; 
                session.Store(item);
                session.SaveChanges();

                return item.Id;
            }
        }

        public void Remove(TodoItem item) => Remove(item.Id);
        public void Remove(string id)
        {
            using (var session = _store.OpenSession())
            {
                session.Delete(id);
                session.SaveChanges();
            }
        }

        public void MarkAsFinished(params string[] taskIds)
        {
            using (var session = _store.OpenSession())
            {
                var tasksToFinish = session.Load<TodoItem>(taskIds);

                foreach(var taskKvp in tasksToFinish)
                {
                    if(taskKvp.Value != null)
                    {
                        //the session tracks entities,
                        //so once SaveChanges() is called, changes are persisted
                        //in a single tx
                        taskKvp.Value.IsFinished = true;
                    }
                }
                session.SaveChanges();
            }
        }

            public Dictionary<DateTime,int> CountTasksPerDate()
        {
            using (var session = _store.OpenSession())
            {
                
                //note: running this query will create map/reduce index
                var result = from todoItem in session.Query<TodoItem>()
                             group todoItem by todoItem.Deadline into g
                             select new
                             {
                                 Deadline = g.Key,
                                 Count = g.Count()
                             };

                return result.ToDictionary(x => x.Deadline, x => x.Count);
            }
        }

        public void RemoveAll()
        {            
            //query for all items and delete all that are found by query
            var result = _store.Operations.Send(
                new DeleteByQueryOperation(
                    new IndexQuery
                    {
                        //RQL query string
                        Query = $"FROM {_store.Conventions.FindCollectionName(typeof(TodoItem))}"
                    }));
            result.WaitForCompletion();
        }

        public IEnumerable<TodoItem> GetItemsWithTodayDeadline() => GetItemsWithDeadline(DateTime.UtcNow.Date);

        public IEnumerable<TodoItem> GetItemsWithDeadline(DateTime deadline)
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<TodoItem>()
                              .Where(t => t.Deadline.Equals(deadline.Date))
                              .ToList();

            }
        }

        public IEnumerable<TodoItem> SearchItemsByName(string searchTerms, SearchOperator @operator = SearchOperator.Or)
        {
            using (var session = _store.OpenSession())
            {
                return session.Advanced
                              .DocumentQuery<TodoItem>()
                              .Search(x => x.Name, searchTerms, @operator)
                              .ToList();
            }
        }

        public TodoItem GetItemById(string id)
        {
            using (var session = _store.OpenSession())
            {
                return session.Load<TodoItem>(id);
            }
        }

        private void CreateDatabaseIfNotExists(string database)
        {
            var databaseNames =
                _store.Admin.Server.Send(new GetDatabaseNamesOperation(0, int.MaxValue));

            if (!databaseNames.Contains(database))
            {
                _store.Admin.Server.Send(
                    new CreateDatabaseOperation(
                        new DatabaseRecord(database)));
            }
        }

        public void Dispose()
        {
            _store?.Dispose();
        }
    }
}
