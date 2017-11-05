using Raven.Client.Documents.Indexes;

namespace ImportCsvData.RavenEntities
{
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