using System;
using AutoMapper;
using ImportBeerDBTemplate.RavenDB;

namespace ImportBeerDBTemplate
{
    class Program
    {
        static void Main(string[] args)
        {
            BeersDBInMemory.LoadDataFromCsv();
            OperationUtils.CreateOpenBeerDBDatabaseIfNeeded();

            Console.WriteLine("Loaded data from CSV, starting import to RavenDB.");
            
            using (var session = DocumentStoreHolder.Store.BulkInsert())
            {
                //session.Store(/*entity instance, entity id*/)
            } //flush the changes

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
