using System;
using System.Linq;
using AutoMapper;
using ImportCsvData.RavenEntities;
using ImportCsvData.Utils;

namespace ImportCsvData
{
    class Program
    {
        static void Main(string[] args)
        {          
            Mapper.Initialize(config =>
            {                
                config.SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
                config.DestinationMemberNamingConvention = new PascalCaseNamingConvention();
                config.CreateMissingTypeMaps = true;
                config.AddProfiles(typeof(Program).Assembly);

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

            DocumentStoreHolder.Store.Database = Configuration.Settings.OpenBeerIngredientsDB;

            new Index_BeerByNameAndBreweryName_StrongTypedDefinition().Execute(DocumentStoreHolder.Store);
            new Index_BeerByNameAndBreweryName_StringDefinition().Execute(DocumentStoreHolder.Store);

            Console.WriteLine();
            Console.WriteLine("---------------------------------");
            Console.WriteLine();

            OperationUtils.CreateDatabaseIfNeeded(Configuration.Settings.OpenBeerIngredientsDB);

            Console.Write("Loading OpenBeerIngredientsDB data from csv files...");
            InMemoryOpenBeerIngredientsDB.LoadData();
            Console.WriteLine("done");

            RavenUtil.ImportEntities(Configuration.Settings.OpenBeerIngredientsDB,
                InMemoryOpenBeerIngredientsDB.Hops.Select(Mapper.Map<Hop>));

            RavenUtil.ImportEntities(Configuration.Settings.OpenBeerIngredientsDB,
                InMemoryOpenBeerIngredientsDB.BJCPCategories.Select(Mapper.Map<BJCPCategories>));

            RavenUtil.ImportEntities(Configuration.Settings.OpenBeerIngredientsDB,
                InMemoryOpenBeerIngredientsDB.Fermentables.Select(Mapper.Map<Fermentables>));

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                session.Store(new Srm
                {
                    ColorsBySrm = InMemoryOpenBeerIngredientsDB.Srm.ToDictionary(x => x.SRM, x => new Srm.Color
                    {
                        R = x.R,
                        G = x.G,
                        B = x.B
                    })
                });
                session.SaveChanges();
            }


                Console.WriteLine();
            Console.WriteLine("---------------------------------");
            Console.WriteLine();

            OperationUtils.CreateDatabaseIfNeeded(Configuration.Settings.SimpsonsDB);

            Console.Write("Loading SimpsonsDB data from csv files...");
            InMemorySimpsonsDB.LoadData();
            Console.WriteLine("done");

            RavenUtil.ImportEntities(Configuration.Settings.SimpsonsDB,
                InMemorySimpsonsDB.Locations.Select(Mapper.Map<Location>));

            RavenUtil.ImportEntities(Configuration.Settings.SimpsonsDB,
                InMemorySimpsonsDB.Characters.Select(Mapper.Map<Character>));

            RavenUtil.ImportEntities(Configuration.Settings.SimpsonsDB,
                InMemorySimpsonsDB.ScriptLines.Select(Mapper.Map<ScriptLine>));

            RavenUtil.ImportEntities(Configuration.Settings.SimpsonsDB,
                InMemorySimpsonsDB.Episodes.Select(Mapper.Map<Episode>));

            Console.WriteLine($"{Environment.NewLine}Finished importing data :)");

        }
    }
}
