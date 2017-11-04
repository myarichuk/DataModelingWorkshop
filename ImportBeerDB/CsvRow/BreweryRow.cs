namespace ImportBeerDB.CsvRow
{
    /*
"id","name","address1","address2","city","state","code","country","phone","website","filepath","descript","last_mod"
     */
    public class BreweryRow
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string code { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public string descript { get; set; }
        public string last_mod { get; set; }
    }
}
