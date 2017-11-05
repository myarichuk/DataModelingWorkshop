using AutoMapper;
using ImportCsvData.CsvRowEntities;

namespace ImportCsvData.RavenEntities
{
    public class CharacterMappingProfile : Profile
    {
        public CharacterMappingProfile()
        {
            CreateMap<SimpsonsCharacterRow, Character>()
                .ForMember(dst => dst.Id, cfg => cfg.ResolveUsing(x => "characters/" + x.id))
                .ForMember(dst => dst.NormalizedName, cfg => cfg.MapFrom(x => x.normalized_name));
        }
    }
}