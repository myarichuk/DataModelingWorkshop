﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ImportBeerDB.CsvRow
{
    /*
        "id","cat_id","style_name","last_mod"
     */
    public class BeerStyleRow
    {
        public int id { get; set; }
        public int cat_id { get; set; }
        public string style_name { get; set; }
        public string last_mod { get; set; }
    }
}
