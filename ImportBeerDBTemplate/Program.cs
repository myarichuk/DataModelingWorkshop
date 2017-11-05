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

            Console.WriteLine("Importing data into " + Configuration.Settings.OpenBeerDB);
            OperationUtils.CreateDatabaseIfNeeded(Configuration.Settings.OpenBeerDB);

            //RavenUtil.ImportEntities(Configuration.Settings.OpenBeerDB, /*entities to import*/);

            Console.WriteLine();
            Console.WriteLine("---------------------------------");
            Console.WriteLine();

            Console.WriteLine("Importing data into " + Configuration.Settings.OpenBeerDataDB);
            OperationUtils.CreateDatabaseIfNeeded(Configuration.Settings.OpenBeerDataDB);

            //RavenUtil.ImportEntities(Configuration.Settings.OpenBeerDataDB, /*entities to import*/);
        }
    }
}
