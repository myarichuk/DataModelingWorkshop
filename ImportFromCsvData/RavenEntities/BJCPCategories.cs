using System.Collections.Generic;

namespace ImportCsvData.RavenEntities
{
    //BJCP --> Beer Judge Certification Program
    public class BJCPCategories
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public List<BJCPSubCategories> Categories { get; set; }
    }
}
