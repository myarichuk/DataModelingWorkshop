using ImportCsvData.Utils;

namespace HandsOnProject
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession(database: Configuration.Settings.SimpsonsDB))
            {
                //queries and 
            }
        }
    }
}
