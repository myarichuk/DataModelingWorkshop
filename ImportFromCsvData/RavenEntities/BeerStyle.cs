using AutoMapper;
using ImportCsvData.CsvRowEntities;

namespace ImportCsvData.RavenEntities
{
    /*
       "id","cat_id","style_name","last_mod"
    */
    public class BeerStyle
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string LastChanged { get; set; }
    }

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
