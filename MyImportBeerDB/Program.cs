using System;
using System.Linq;
using AutoMapper;
using ImportBeerDBTemplate.Utils;
using MyImportBeerDB.RavenEntities;

namespace ImportBeerDBTemplate
{
    class Program
    {
        static void Main(string[] args)
        {          
            Mapper.Initialize(config =>
            {                
                config.AddProfiles(typeof(Program).Assembly);
                config.CreateMissingTypeMaps = true;
            });

            Console.WriteLine();
            Console.Write("Loading OpenBeerDB data from csv files...");
            InMemoryOpenBeerDB.LoadData();
            Console.WriteLine("done");

            Console.WriteLine("Importing data into " + Configuration.Settings.OpenBeerDB);
            OperationUtils.CreateDatabaseIfNeeded(Configuration.Settings.OpenBeerDB);

            RavenUtil.ImportEntities(Configuration.Settings.OpenBeerDB,
                InMemoryOpenBeerDB.Breweries.Select(Mapper.Map<Brewery>));

            RavenUtil.ImportEntities(Configuration.Settings.OpenBeerDB,
                InMemoryOpenBeerDB.BeerCategories.Select(Mapper.Map<BeerCategory>));

            RavenUtil.ImportEntities(Configuration.Settings.OpenBeerDB,
                InMemoryOpenBeerDB.BeerStyles.Select(Mapper.Map<BeerStyle>));

            RavenUtil.ImportEntities(Configuration.Settings.OpenBeerDB,
                InMemoryOpenBeerDB.Beers.Select(Mapper.Map<Beer>));

            DocumentStoreHolder.Store.Database = Configuration.Settings.OpenBeerDataDB;

            new Index_BeerByNameAndBreweryName_StrongTypedDefinition().Execute(DocumentStoreHolder.Store);
            new Index_BeerByNameAndBreweryName_StringDefinition().Execute(DocumentStoreHolder.Store);

            Console.WriteLine();
            Console.WriteLine("---------------------------------");
            Console.WriteLine();

            OperationUtils.CreateDatabaseIfNeeded(Configuration.Settings.OpenBeerDataDB);

            Console.Write("Loading OpenBeerDataDB data from csv files...");
            InMemoryOpenBeerDataDB.LoadData();
            Console.WriteLine("done");

            RavenUtil.ImportEntities(Configuration.Settings.OpenBeerDataDB,
                InMemoryOpenBeerDataDB.Hops.Select(Mapper.Map<Hops>));

            RavenUtil.ImportEntities(Configuration.Settings.OpenBeerDataDB,
                InMemoryOpenBeerDataDB.BJCPCategories.Select(Mapper.Map<BJCPCategories>));

            RavenUtil.ImportEntities(Configuration.Settings.OpenBeerDataDB,
                InMemoryOpenBeerDataDB.Fermentables.Select(Mapper.Map<Fermentables>));

            RavenUtil.ImportEntities(Configuration.Settings.OpenBeerDataDB,
                InMemoryOpenBeerDataDB.Srm.Select(Mapper.Map<Srm>));

            Console.WriteLine($"{Environment.NewLine}Finished importing data :)");
        }
    }
}
