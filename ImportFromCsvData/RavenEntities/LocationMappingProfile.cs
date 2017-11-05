using AutoMapper;
using ImportCsvData.CsvRowEntities;

namespace ImportCsvData.RavenEntities
{
    public class LocationMappingProfile : Profile
    {
        public LocationMappingProfile()
        {
            CreateMap<SimpsonsLocationRow, Location>()
                .ForMember(dst => dst.Id, cfg => cfg.ResolveUsing(x => "locations/" + x.id))
                .ForMember(dst => dst.NormalizedName, cfg => cfg.MapFrom(x => x.normalized_name));
        }
    }
}