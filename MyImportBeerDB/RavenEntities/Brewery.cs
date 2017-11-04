using System;
using AutoMapper;
using ImportBeerDBTemplate.CsvRowEntities;

namespace MyImportBeerDB.RavenEntities
{
    public class Brewery
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Code { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
        public DateTime LastModified { get; set; }
    }

    public class BreweryMappingProfiler : Profile
    {
        public BreweryMappingProfiler()
        {
            CreateMap<BreweryRow, Brewery>().ForMember(dst => dst.Id, 
                cfg => cfg.ResolveUsing(x => "breweries/" + x.id));
        }
    }
}

/*
 * 1)parsing of RQL
 * 2)index selection/creation
 * 3)filtering - running where (Lucene.Net)
 * 4)sorting
 * 5)load documents (if needed)
 * 6)projection (Script Runner)
 * 7)includes
 */