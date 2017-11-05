using AutoMapper;

namespace MyImportBeerDB.RavenEntities
{
    public class Fermentables
    {
        public string Type { get; set; }
        public string Supplier { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public double Color { get; set; }
    }

    public class FermentablesMapProfile : Profile
    {
    }
}
