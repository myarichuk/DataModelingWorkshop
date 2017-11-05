using System.Linq;
using AutoMapper;
using ImportCsvData.CsvRowEntities;

namespace ImportCsvData.RavenEntities
{
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