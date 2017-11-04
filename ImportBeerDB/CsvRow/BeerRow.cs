using System;
using CsvHelper.Configuration;

namespace ImportBeerDB.CsvRow
{
    /*
     id	brewery_id	name	cat_id	style_id	abv	ibu	srm	upc	filepath	descript	last_mod
     */
    public class BeerRow
    {
        public int id { get; set; }
        public int brewery_id { get; set; }
        public string name { get; set; }
        public int cat_id { get; set; }
        public double abv { get; set; }
        public double ibu { get; set; }
        public double srm { get; set; }
        public double upc { get; set; }
        public string descript { get; set; }
        public string last_mod { get; set; }
    }

    public sealed class BeerRowMap : ClassMap<BeerRow>
    {
        public BeerRowMap()
        {
            Map(m => m.id).Name(nameof(BeerRow.id)).Index(0).Default(-1);
            Map(m => m.brewery_id).Name(nameof(BeerRow.brewery_id)).Index(1).Default(-1);
            Map(m => m.name).Name(nameof(BeerRow.name)).Index(2);
            Map(m => m.cat_id).Name(nameof(BeerRow.cat_id)).Index(3).Default(-1);
            Map(m => m.abv).Name(nameof(BeerRow.abv)).Index(4).Default(0.0);
            Map(m => m.ibu).Name(nameof(BeerRow.ibu)).Index(5).Default(0.0);
            Map(m => m.srm).Name(nameof(BeerRow.srm)).Index(6).Default(0.0);
            Map(m => m.upc).Name(nameof(BeerRow.upc)).Index(7).Default(0.0);
            Map(m => m.descript).Name(nameof(BeerRow.descript)).Index(8);
            Map(m => m.last_mod).Name(nameof(BeerRow.last_mod)).Index(9);
        }
    }

}
