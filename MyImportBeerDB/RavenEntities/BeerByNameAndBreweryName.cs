using System.Linq;
using Raven.Client;
using Raven.Client.Documents.Indexes;

namespace MyImportBeerDB.RavenEntities
{
    /*
        Example of RQL querying an index:
        
        from index 'Index/BeerByNameAndBreweryName/StrongTypedDefinition' as i
        where Search(i.Beer, "Pils") and i.Country = "United States"

     */
    public class Index_BeerByNameAndBreweryName_StrongTypedDefinition : AbstractIndexCreationTask<Beer>
    {
        public Index_BeerByNameAndBreweryName_StrongTypedDefinition()
        {
            Map = beers => 
                from beer in beers
                let brewery = LoadDocument<Brewery>(beer.Brewery)
                select new
                {
                    Beer = beer.Name,
                    Brewery = brewery.Name,
                    brewery.Country,
                };
            
            Analyze("Beer","StandardAnalyzer");
        }
    }

    public class Index_BeerByNameAndBreweryName_StringDefinition : AbstractIndexCreationTask
    {
        public override string IndexName => "Index/BeerByNameAndBreweryName/StringDefinition";

        public override IndexDefinition CreateIndexDefinition()
        {
            return new IndexDefinition
            {
                Maps =
                {
                    @"from beer in docs.Beers
                    let brewery = this.LoadDocument(beer.Brewery,""Breweries"")
                    select new 
                    {
                        Country = brewery.Country,
                        Beer = beer.Name,
                        Brewery = brewery.Name
                    }"
                },
                Fields =
                {
                    {"Beer",
                        new IndexFieldOptions
                        {
                            Analyzer = @"StandardAnalyzer"
                        } 
                    }
                }
            };
        }
    }
}

