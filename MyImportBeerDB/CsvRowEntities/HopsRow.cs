namespace ImportBeerDBTemplate.CsvRowEntities
{
    //_id,name,origin,alpha_low,alpha_high,notes
    public class HopsRow
    {
        public int _id { get; set; }
        public string name { get; set; }
        public string origin { get; set; }
        public double alpha_low { get; set; }
        public double alpha_high { get; set; }
        public string notes { get; set; }
    }
}
