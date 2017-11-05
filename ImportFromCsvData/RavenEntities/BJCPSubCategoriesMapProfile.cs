using AutoMapper;
using ImportCsvData.CsvRowEntities;

namespace ImportCsvData.RavenEntities
{
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