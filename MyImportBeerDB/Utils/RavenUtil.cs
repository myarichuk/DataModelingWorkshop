using System;
using System.Collections.Generic;

namespace ImportBeerDBTemplate.Utils
{
    public static class RavenUtil
    {
        public static void ImportEntities<TEntity>(string database, IEnumerable<TEntity> entities)
        {
            Console.Write($"Importing {typeof(TEntity).Name}...");
            using (var bulkInsert = DocumentStoreHolder.Store.BulkInsert(database))
            {
                foreach (var entity in entities)
                    bulkInsert.Store(entity);
            }
            Console.WriteLine("done");
        }
    }
}
