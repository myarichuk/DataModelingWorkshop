namespace ImportCsvData.RavenEntities
{
    /*
        "id","cat_name","last_mod"
     */

    public class BeerCategory
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastChanged { get; set; }
    }
}
