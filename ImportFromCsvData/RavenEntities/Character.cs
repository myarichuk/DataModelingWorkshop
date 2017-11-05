using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ImportCsvData.CsvRowEntities;

namespace ImportCsvData.RavenEntities
{
    public class Character
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string Gender { get; set; }
    }
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
