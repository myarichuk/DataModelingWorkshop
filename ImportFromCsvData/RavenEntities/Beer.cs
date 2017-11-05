namespace ImportCsvData.RavenEntities
{
    public class Beer
    {
        public string Id { get; set; }
        public string Brewery { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string CategoryName { get; set; }
        public string Style { get; set; }
        public string StyleName { get; set; }
        public double Abv { get; set; }
        public double Ibu { get; set; }
        public double Srm { get; set; }
        public double Upc { get; set; }
        public string Description { get; set; }
    }
}
