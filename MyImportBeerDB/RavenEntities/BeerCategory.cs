using AutoMapper;
using ImportBeerDBTemplate.CsvRowEntities;

namespace MyImportBeerDB.RavenEntities
{
    /*
        "id","cat_name","last_mod"
     */

    public class BeerCategory
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastChanged { get; set; }
    }

    public class BeerCategoryMappingProfile : Profile
    {
        public BeerCategoryMappingProfile()
        {
            CreateMap<BeerCategoryRow, BeerCategory>()
                .ForMember(dst => dst.Id, cfg => cfg.ResolveUsing(x => "beercategories/" + x.id))
                .ForMember(dst => dst.Name, cfg => cfg.MapFrom(src => src.cat_name))
                .ForMember(dst => dst.LastChanged, cfg => cfg.MapFrom(src => src.last_mod));
        }
    }
}
