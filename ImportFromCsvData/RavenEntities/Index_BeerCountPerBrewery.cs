using Raven.Client.Documents.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImportCsvData.RavenEntities
{
    public class Index_BeerCountPerBrewery : AbstractIndexCreationTask<Beer, 
                                                    Index_BeerCountPerBrewery.BeerCountPerBreweryResult>
    {
        public class BeerCountPerBreweryResult
        {
            public string BreweryId { get; set; }
            public int Count { get; set; }
        }

        public Index_BeerCountPerBrewery()
        {
            Map = beers => from beer in beers
                           select new
                           {
                               BreweryId = beer.Brewery,
                               Count = 1
                           };

            Reduce = results => from result in results
                     group result by result.BreweryId into g
                     select new
                     {
                         BreweryId = g.Key,
                         Count = g.Sum(x => x.Count)
                     };
        }
    }

    
}
