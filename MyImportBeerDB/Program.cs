using System;
using System.Linq;
using AutoMapper;
using ImportBeerDBTemplate.CsvRowEntities;
using ImportBeerDBTemplate.Utils;
using MyImportBeerDB.RavenEntities;

namespace ImportBeerDBTemplate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Loading OpenBeerDB data from csv files...");
            InMemoryOpenBeerDB.LoadData();
            Console.WriteLine("done");

            Console.Write("Loading OpenBeerDataDB data from csv files...");
            InMemoryOpenBeerDataDB.LoadData();
            Console.WriteLine("done");

            Mapper.Initialize(config =>
            {                
                config.AddProfiles(typeof(Program).Assembly);
            });


            OperationUtils.CreateDatabaseIfNeeded(Configuration.Settings.OpenBeerDB);

            RavenUtil.ImportEntities(Configuration.Settings.OpenBeerDB,
                InMemoryOpenBeerDB.Breweries.Select(Mapper.Map<Brewery>));

            RavenUtil.ImportEntities(Configuration.Settings.OpenBeerDB,
                InMemoryOpenBeerDB.BeerCategories.Select(Mapper.Map<BeerCategory>));

            RavenUtil.ImportEntities(Configuration.Settings.OpenBeerDB,
                InMemoryOpenBeerDB.BeerStyles.Select(Mapper.Map<BeerStyle>));

            RavenUtil.ImportEntities(Configuration.Settings.OpenBeerDB,
                InMemoryOpenBeerDB.Beers.Select(Mapper.Map<Beer>));

            //OperationUtils.CreateDatabaseIfNeeded(Configuration.Settings.OpenBeerDataDB);

            Console.WriteLine($"{Environment.NewLine} Finished importing data :)");
        }
    }
}
