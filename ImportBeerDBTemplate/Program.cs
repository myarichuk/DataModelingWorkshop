using System;
using System.IO.MemoryMappedFiles;
using ImportBeerDBTemplate.Utils;

namespace ImportBeerDBTemplate
{
    class Program
    {
        static void Main()
        {
            Console.Write("Loading OpenBeerDB data from csv files...");
            InMemoryOpenBeerDB.LoadData();
            Console.WriteLine("done");

            Console.Write("Loading OpenBeerDataDB data from csv files...");
            InMemoryOpenBeerDataDB.LoadData();
            Console.WriteLine("done");

            OperationUtils.CreateDatabaseIfNeeded(Configuration.Settings.OpenBeerDB);
            OperationUtils.CreateDatabaseIfNeeded(Configuration.Settings.OpenBeerDataDB);

        }
    }
}
