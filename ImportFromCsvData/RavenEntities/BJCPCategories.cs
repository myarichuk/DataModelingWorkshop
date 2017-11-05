using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ImportCsvData.CsvRowEntities;

namespace ImportCsvData.RavenEntities
{
    //BJCP --> Beer Judge Certification Program
    public class BJCPCategories
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public List<BJCPSubCategories> Categories { get; set; }
    }

    public class BJCPCategoriesMapProfile : Profile
    {
        public BJCPCategoriesMapProfile()
        {
            CreateMap<BJCPCategoryRow, BJCPCategories>()
                .ForMember(dst => dst.Id, cfg => cfg.ResolveUsing(x => "bjcpcategories/" + x._id))
                .ForMember(dst => dst.Categories, cfg => cfg.ResolveUsing(x => 
                        InMemoryOpenBeerDataDB.BJCPSubCategories.Select(Mapper.Map<BJCPSubCategories>).ToList()));
        }
    }
}
