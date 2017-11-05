using AutoMapper;
using ImportCsvData.CsvRowEntities;

namespace ImportCsvData.RavenEntities
{
    public class BeerStyleMappingProfile : Profile
    {
        public BeerStyleMappingProfile()
        {
            CreateMap<BeerStyleRow, BeerStyle>()
                .ForMember(dst => dst.Id, cfg => cfg.ResolveUsing(x => "beerstyles/" + x.id))
                .ForMember(dst => dst.Name, cfg => cfg.MapFrom(src => src.style_name))
                .ForMember(dst => dst.Category, cfg => cfg.ResolveUsing(x => "beercategories/" + x.cat_id))
                .ForMember(dst => dst.LastChanged, cfg => cfg.MapFrom(src => src.last_mod));
        }
    }
}