namespace ImportBeerDBTemplate.CsvRowEntities
{
    public class BJCPSubCategories
    {
        /*
        _id,
        bjcp_category_id,
        display_id,
        name,
        aroma,
        appearance,
        flavor,
        mouthfeel,
        impression,
        comments,
        ingredients,
        og_low,
        og_high,
        fg_low,
        fg_high,
        ibu_low,
        ibu_high,
        srm_low,
        srm_high,
        abv_low,
        abv_high,
        examples
        */
        public int _id { get; set; }
        public int bjcp_category_id { get; set; }
        public string display_id { get; set; }
        public string name { get; set; }
        public string aroma { get; set; }
        public string appearance { get; set; }
        public string flavor { get; set; }
        public string mouthfeel { get; set; }
        public string impression { get; set; }
        public string comments { get; set; }
        public string ingredients { get; set; }

        public double og_low { get; set; }
        public double og_high { get; set; }
        public double fg_low { get; set; }
        public double fg_high { get; set; }
        public double srm_low { get; set; }
        public double srm_high { get; set; }
        public double ibu_low { get; set; }
        public double ibu_high { get; set; }
        public double abv_low { get; set; }
        public double abv_high { get; set; }

        public string examples { get; set; }
    }
}