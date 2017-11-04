using System.Linq;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace ImportBeerDBTemplate.RavenUtils
{
    public static class OperationUtils
    {
        public static void CreateDatabaseIfNeeded(string databaseName)
        {
            var databaseNames =
                DocumentStoreHolder.Store.Admin.Server.Send(new GetDatabaseNamesOperation(0, int.MaxValue));

            if (!databaseNames.Contains(databaseName))
            {
                DocumentStoreHolder.Store.Admin.Server.Send(
                    new CreateDatabaseOperation(
                        new DatabaseRecord(databaseName)));
            }
        }
    }
}
