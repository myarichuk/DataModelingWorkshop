using System;
using System.Linq;
using Raven.Client.Documents.Queries;
using Xunit;

namespace TodoListLib.Tests
{
    public class TodoListTests : IDisposable
    {
        private readonly TodoManager _todoManager = new TodoManager("http://localhost:8080","TodoListDB");

        public TodoListTests()
        {
            _todoManager.RemoveAll();
        }

        [Fact]
        public void Count_tasks_per_date_should_work()
        {
            _todoManager.Add(new TodoItem
            {
                Name = "Buy groceries",
                Deadline = DateTime.UtcNow.AddDays(3)
            });

            _todoManager.Add(new TodoItem
            {
                Name = "Wash car",
                Deadline = DateTime.UtcNow.AddDays(3)
            });

            _todoManager.Add(new TodoItem
            {
                Name = "Don't forget to buy presents for myself",
                Deadline = DateTime.UtcNow.AddDays(7)
            });

            _todoManager.Add(new TodoItem
            {
                Name = "Play Fallout 4",
                Deadline = DateTime.UtcNow.AddDays(7)
            });

            _todoManager.Add(new TodoItem
            {
                Name = "Don't forget to buy flowers for wife",
                Deadline = DateTime.UtcNow.AddDays(7)
            });

            var taskCountPerDeadline = _todoManager.CountTasksPerDate();
            Assert.Equal(2, taskCountPerDeadline.Count);
            Assert.Equal(3, taskCountPerDeadline[DateTime.UtcNow.AddDays(7).Date]);
            Assert.Equal(2, taskCountPerDeadline[DateTime.UtcNow.AddDays(3).Date]);
        }

        [Fact]
        public void Can_add_item()
        {
            var itemId = _todoManager.Add(new TodoItem
            {
                Name = "Test Task",
                Deadline = DateTime.UtcNow.AddDays(3)
            });

            var fetchedItem = _todoManager.GetItemById(itemId);
            Assert.NotNull(fetchedItem);
            Assert.Equal("Test Task",fetchedItem.Name);
        }

        [Fact]
        public void Can_remove_item()
        {
            var itemId = _todoManager.Add(new TodoItem
            {
                Name = "Test Task",
                Deadline = DateTime.UtcNow.AddDays(3)
            });

            var fetchedItem = _todoManager.GetItemById(itemId);
            Assert.NotNull(fetchedItem); //sanity check

            _todoManager.Remove(fetchedItem.Id);

            Assert.Null(_todoManager.GetItemById(itemId));
        }

        [Fact]
        public void Can_find_item_by_name()
        {
            _todoManager.Add(new TodoItem
            {
                Name = "Buy groceries",
                Deadline = DateTime.UtcNow.AddDays(3)
            });

            _todoManager.Add(new TodoItem
            {
                Name = "Wash car",
                Deadline = DateTime.UtcNow.AddDays(7)
            });

            _todoManager.Add(new TodoItem
            {
                Name = "Don't forget to buy presents for myself",
                Deadline = DateTime.UtcNow.AddDays(30)
            });

            _todoManager.Add(new TodoItem
            {
                Name = "Don't forget to buy flowers for wife",
                Deadline = DateTime.UtcNow.AddDays(30)
            });

            _todoManager.Add(new TodoItem
            {
                Name = "Call mom",
                Deadline = DateTime.UtcNow.AddDays(2)
            });

            var searchResults = _todoManager.SearchItemsByName("flowers").ToList();

            Assert.Equal(1,searchResults.Count);
            Assert.Equal("Don't forget to buy flowers for wife", searchResults[0].Name);

            //note: here by default, SearchOperator is OR
            searchResults = _todoManager.SearchItemsByName("buy flowers").ToList();
            Assert.Equal(3, searchResults.Count);
            Assert.Contains("Don't forget to buy presents for myself", searchResults.Select(x => x.Name));
            Assert.Contains("Buy groceries", searchResults.Select(x => x.Name));
            Assert.Contains("Don't forget to buy flowers for wife", searchResults.Select(x => x.Name));

            searchResults = _todoManager.SearchItemsByName("buy presents",SearchOperator.And).ToList();

            Assert.Equal(1, searchResults.Count);
            Assert.Equal("Don't forget to buy presents for myself", searchResults[0].Name);
        }

        [Fact]
        public void Can_get_items_with_specific_deadline()
        {
            _todoManager.Add(new TodoItem
            {
                Name = "Buy groceries",
                Deadline = DateTime.UtcNow.AddDays(3)
            });

            _todoManager.Add(new TodoItem
            {
                Name = "Wash car",
                Deadline = DateTime.UtcNow.AddDays(3)
            });

            _todoManager.Add(new TodoItem
            {
                Name = "Don't forget to buy presents for myself",
                Deadline = DateTime.UtcNow.AddDays(7)
            });

            _todoManager.Add(new TodoItem
            {
                Name = "Don't forget to buy flowers for wife",
                Deadline = DateTime.UtcNow.AddDays(7)
            });

            var searchResults = _todoManager.GetItemsWithDeadline(DateTime.UtcNow.AddDays(7)).ToList();
            Assert.Equal(2, searchResults.Count);
            Assert.Contains("Don't forget to buy presents for myself", searchResults.Select(x => x.Name));
            Assert.Contains("Don't forget to buy flowers for wife", searchResults.Select(x => x.Name));
        }

        public void Dispose()
        {
            _todoManager.Dispose();
        }
    }
}
