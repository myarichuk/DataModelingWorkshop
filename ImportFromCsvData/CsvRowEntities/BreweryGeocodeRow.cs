namespace ImportCsvData.CsvRowEntities
{
    /*
        "id","brewery_id","latitude","longitude","accuracy"
    */
    public class BreweryGeocodeRow
    {
        public int id { get; set; }
        public int brewery_id { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string accuracy { get; set; }
    }
}
