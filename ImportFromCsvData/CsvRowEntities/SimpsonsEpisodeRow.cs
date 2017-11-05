namespace ImportCsvData.CsvRowEntities
{
    public class SimpsonsEpisodeRow
    {
        public int id { get; set; }
        public string title { get; set; }
        public string original_air_date { get; set; }
        public string production_code { get; set; }
        public int? season { get; set; }
        public int? number_in_season { get; set; }
        public int? number_in_series { get; set; }
        public double? us_viewers_in_millions { get; set; }
        public double? views { get; set; }
        public double? imdb_rating { get; set; }
        public double? imdb_votes { get; set; }
        public string image_url { get; set; }
        public string video_url { get; set; }
    }
}
