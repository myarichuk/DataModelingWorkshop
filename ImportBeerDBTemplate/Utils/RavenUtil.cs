using System.Collections.Generic;
using ImportBeerDBTemplate.RavenUtils;

namespace ImportBeerDBTemplate.Utils
{
    public static class RavenUtil
    {
        public static void ImportEntities<TEntity>(string database, IEnumerable<TEntity> entities)
        {
            using (var bulkInsert = DocumentStoreHolder.Store.BulkInsert(database))
            {
                foreach (var entity in entities)
                    bulkInsert.Store(entity);
            }
        }
    }
}
