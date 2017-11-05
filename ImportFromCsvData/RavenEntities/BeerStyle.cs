namespace ImportCsvData.RavenEntities
{
    /*
       "id","cat_id","style_name","last_mod"
    */
    public class BeerStyle
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string LastChanged { get; set; }
    }
}
