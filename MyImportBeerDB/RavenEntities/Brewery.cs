using System;
using System.Linq;
using AutoMapper;
using ImportBeerDBTemplate;
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
        public LocationCode Location { get; set; }

        public class LocationCode
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public string Accuracy { get; set; }
        }
    }

    public class BreweryMappingProfiler : Profile
    {
        public BreweryMappingProfiler()
        {
            CreateMap<BreweryRow, Brewery>()
                .ForMember(dst => dst.Id, cfg => cfg.ResolveUsing(x => "breweries/" + x.id))
                .ForMember(dst => dst.Description, cfg => cfg.MapFrom(src => src.descript))
                .ForMember(dst => dst.Location, cfg => cfg.ResolveUsing(src =>
                {
                    var geocode = InMemoryOpenBeerDB.BreweryGeocodes.FirstOrDefault(g => g.brewery_id == src.id);
                    if (geocode == null)
                        return null;

                    return new Brewery.LocationCode
                    {
                        Longitude = geocode.longitude,
                        Latitude = geocode.latitude,
                        Accuracy = geocode.accuracy
                    };
                }));
        }
    }
}