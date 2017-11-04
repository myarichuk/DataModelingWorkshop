using System.Linq;
using ImportBeerDBTemplate.RavenDB;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace ImportBeerDBTemplate
{
    class Program
    {
        static void Main()
        {
            BeersDBInMemory.LoadDataFromCsv();
            CreateOpenBeerDBDatabaseIfNeeded();

            using (var session = DocumentStoreHolder.Store.BulkInsert())
            {
                //session.Store(/*entity instance, entity id*/)
            } //flush the changes
        }

        static void CreateOpenBeerDBDatabaseIfNeeded()
        {
            var databaseNames =
                DocumentStoreHolder.Store.Admin.Server.Send(new GetDatabaseNamesOperation(0, int.MaxValue));

            if (!databaseNames.Contains(Configuration.Settings.Database))
            {
                DocumentStoreHolder.Store.Admin.Server.Send(
                    new CreateDatabaseOperation(
                        new DatabaseRecord(Configuration.Settings.Database)));
            }
        }
    }
}
