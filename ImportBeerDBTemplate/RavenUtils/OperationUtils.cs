using System.Linq;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace ImportBeerDBTemplate.RavenDB
{
    public static class OperationUtils
    {
        public static void CreateOpenBeerDBDatabaseIfNeeded()
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
