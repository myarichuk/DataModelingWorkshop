using System.Linq;
using AutoMapper;
using ImportCsvData.CsvRowEntities;

namespace ImportCsvData.RavenEntities
{
    public class BeerMappingProfile : Profile
    {
        public BeerMappingProfile()
        {
            CreateMap<BeerRow, Beer>()
                .ForMember(dst => dst.Id, cfg => cfg.ResolveUsing(x => "beers/" + x.id))
                .ForMember(dst => dst.Description, cfg => cfg.MapFrom(src => src.descript))
                .ForMember(dst => dst.Brewery, cfg => cfg.ResolveUsing(x => "breweries/" + x.brewery_id))
                .ForMember(dst => dst.Category, cfg => cfg.ResolveUsing(x => "beercategories/" + x.cat_id))
                .ForMember(dst => dst.CategoryName, cfg => cfg.ResolveUsing(x =>
                {
                    var category = InMemoryOpenBeerDB.BeerCategories.FirstOrDefault(br => br.id == x.cat_id);
                    return category?.cat_name;
                }))
                .ForMember(dst => dst.Style, cfg => cfg.ResolveUsing(x => "beerstyles/" + InMemoryOpenBeerDB.BeerStyles.FirstOrDefault(br => br.id == x.cat_id)?.id))
                .ForMember(dst => dst.StyleName, cfg => cfg.ResolveUsing(x =>
                {
                    var style = InMemoryOpenBeerDB.BeerStyles.FirstOrDefault(br => br.id == x.cat_id);
                    return style?.style_name;
                }));
        }
    }
}