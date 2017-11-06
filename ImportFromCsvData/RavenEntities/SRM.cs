using System.Collections.Generic;

namespace ImportCsvData.RavenEntities
{
    public class Srm
    {
        public class Color
        {
            public double R { get; set; }
            public double G { get; set; }
            public double B { get; set; }
        }
        public Dictionary<double,Color> ColorsBySrm { get; set; }
    }   
}
