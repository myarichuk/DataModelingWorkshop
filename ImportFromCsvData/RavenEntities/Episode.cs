namespace ImportCsvData.RavenEntities
{
    public class Episode
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string OriginalAirDate { get; set; }
        public string ProductionCode { get; set; }
        public int Season { get; set; }
        public double NumberInSeason { get; set; }
        public double NumberInSeries { get; set; }
        public double UsViewersInMillions { get; set; }
        public double Views { get; set; }
        public double ImdbRating { get; set; }
        public double ImdbVotes { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
    }
}
