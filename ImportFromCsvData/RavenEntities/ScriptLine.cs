using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using ImportCsvData.CsvRowEntities;

namespace ImportCsvData.RavenEntities
{
    public class ScriptLine
    {
        public string Id { get; set; }
        public string Episode { get; set; }
        public int? Number { get; set; }
        public string RawText { get; set; }
        public int TimestampInMs { get; set; }
        public string SpeakingLine { get; set; }
        public string Character { get; set; }
        public string Location { get; set; }
        public string RawCharacterText { get; set; }
        public string RawLocationText { get; set; }
        public string SpokenWords { get; set; }
        public string NormalizedText { get; set; }
        public int? WordCount { get; set; }
    }

    public class ScriptLineMappingProfile : Profile
    {
        public ScriptLineMappingProfile()
        {
            CreateMap<SimponsScriptLinesRow, ScriptLine>()
                .ForMember(dst => dst.Id, cfg => cfg.ResolveUsing(x => "scriptlines/" + x.id))
                .ForMember(dst => dst.Episode, cfg => cfg.ResolveUsing(x => "episodes/" + x.episode_id))
                .ForMember(dst => dst.Character, cfg => cfg.ResolveUsing(x => x.character_id.HasValue == false ? null : "characters/" + x.character_id))
                .ForMember(dst => dst.Location, cfg => cfg.ResolveUsing(x => "locations/" + x.location_id))
                .ForMember(dst => dst.RawText, cfg => cfg.MapFrom(x => x.raw_text))
                .ForMember(dst => dst.TimestampInMs, cfg => cfg.MapFrom(x => x.timestamp_in_ms))
                .ForMember(dst => dst.SpeakingLine, cfg => cfg.MapFrom(x => x.speaking_line))
                .ForMember(dst => dst.RawCharacterText, cfg => cfg.MapFrom(x => x.raw_character_text))
                .ForMember(dst => dst.RawLocationText, cfg => cfg.MapFrom(x => x.raw_location_text))
                .ForMember(dst => dst.SpokenWords, cfg => cfg.MapFrom(x => x.spoken_words))
                .ForMember(dst => dst.NormalizedText, cfg => cfg.MapFrom(x => x.normalized_text))
                .ForMember(dst => dst.WordCount, cfg => cfg.MapFrom(x => x.word_count ?? 0));
        }
    }
}
