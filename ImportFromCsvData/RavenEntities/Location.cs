using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ImportCsvData.CsvRowEntities;

namespace ImportCsvData.RavenEntities
{
    public class Location
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
    }

    public class LocationMappingProfile : Profile
    {
        public LocationMappingProfile()
        {
            CreateMap<SimpsonsLocationRow, Location>()
                .ForMember(dst => dst.Id, cfg => cfg.ResolveUsing(x => "locations/" + x.id))
                .ForMember(dst => dst.NormalizedName, cfg => cfg.MapFrom(x => x.normalized_name));
        }
    }
}
