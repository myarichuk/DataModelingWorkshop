using System.Linq;
using Raven.Client.Documents.Indexes;

namespace ImportCsvData.RavenEntities
{
    /*
        Example of RQL searching on an index:
        
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
}

