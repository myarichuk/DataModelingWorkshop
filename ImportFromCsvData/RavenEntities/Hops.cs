using System.Collections.Generic;

namespace ImportCsvData.RavenEntities
{
    public class Hop
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Origin { get; set; }
        public double AlphaLow { get; set; }
        public double AlphaHigh { get; set; }
        public string Notes { get; set; }
        public Dictionary<string,string> SubstituteIdByName { get; set; }
    }
}
