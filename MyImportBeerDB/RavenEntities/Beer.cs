using System.Linq;
using AutoMapper;
using ImportBeerDBTemplate;
using ImportBeerDBTemplate.CsvRowEntities;

namespace MyImportBeerDB.RavenEntities
{
    public class Beer
    {
        public string Id { get; set; }
        public string Brewery { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string StyleName { get; set; }
        public double Abv { get; set; }
        public double Ibu { get; set; }
        public double Srm { get; set; }
        public double Upc { get; set; }
        public string Description { get; set; }
    }

    public class BeerMappingProfile : Profile
    {
        public BeerMappingProfile()
        {
            CreateMap<BeerRow, Beer>()
                .ForMember(dst => dst.Id, cfg => cfg.ResolveUsing(x => "beers/" + x.id))
                .ForMember(dst => dst.Description, cfg => cfg.MapFrom(src => src.descript))
                .ForMember(dst => dst.Brewery, cfg => cfg.ResolveUsing(x =>
                {
                    var brewery = InMemoryOpenBeerDB.Breweries.FirstOrDefault(br => br.id == x.brewery_id);
                    return "breweries/" + brewery?.id;
                }))
                .ForMember(dst => dst.CategoryName, cfg => cfg.ResolveUsing(x =>
                {
                    var category = InMemoryOpenBeerDB.BeerCategories.FirstOrDefault(br => br.id == x.cat_id);
                    return category?.cat_name;
                }))
                .ForMember(dst => dst.StyleName, cfg => cfg.ResolveUsing(x =>
                {
                    var style = InMemoryOpenBeerDB.BeerStyles.FirstOrDefault(br => br.id == x.cat_id);
                    return style?.style_name;
                }));
        }
    }
}
