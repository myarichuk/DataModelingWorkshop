using AutoMapper;
using ImportCsvData.RavenEntities;

namespace ImportCsvData.CsvRowEntities
{
    public class SimponsScriptLinesRow
    {
        public int id { get; set; }
        public int episode_id { get; set; }
        public int? number { get; set; }
        public string raw_text { get; set; }
        public int? timestamp_in_ms { get; set; }
        public string speaking_line { get; set; }
        public int? character_id { get; set; }
        public int? location_id { get; set; }
        public string raw_character_text { get; set; }
        public string raw_location_text { get; set; }
        public string spoken_words { get; set; }
        public string normalized_text { get; set; }
        public int? word_count { get; set; }            
    }   
}
