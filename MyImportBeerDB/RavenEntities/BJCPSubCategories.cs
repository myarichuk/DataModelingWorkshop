using AutoMapper;
using ImportBeerDBTemplate.CsvRowEntities;

namespace MyImportBeerDB.RavenEntities
{
    public class BJCPSubCategories
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Aroma { get; set; }
        public string Appearance { get; set; }
        public string Flavor { get; set; }
        public string Mouthfeel { get; set; }
        public string Impression { get; set; }
        public string Comments { get; set; }
        public string[] Ingredients { get; set; }

        public double og_low { get; set; }
        public double og_high { get; set; }
        public double fg_low { get; set; }
        public double fg_high { get; set; }
        public double srm_low { get; set; }
        public double srm_high { get; set; }
        public double ibu_low { get; set; }
        public double ibu_high { get; set; }
        public double abv_low { get; set; }
        public double abv_high { get; set; }

        public string[] Examples { get; set; }
    }

    public class BJCPSubCategoriesMapProfile : Profile
    {
        public BJCPSubCategoriesMapProfile()
        {
            CreateMap<BJCPSubCategoryRow, BJCPSubCategories>()
                .ForMember(dst => dst.Id, cfg => cfg.ResolveUsing(x => "bjcpsubcategories/" + x._id))
                .ForMember(dst => dst.Ingredients, cfg => cfg.ResolveUsing(x => x.ingredients.Split(";")))
                .ForMember(dst => dst.Examples, cfg => cfg.ResolveUsing(x => x.examples.Split(";")));
        }
    }
}