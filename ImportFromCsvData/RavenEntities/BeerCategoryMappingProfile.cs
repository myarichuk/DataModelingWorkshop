﻿using AutoMapper;
using ImportCsvData.CsvRowEntities;

namespace ImportCsvData.RavenEntities
{
    public class BeerCategoryMappingProfile : Profile
    {
        public BeerCategoryMappingProfile()
        {
            CreateMap<BeerCategoryRow, BeerCategory>()
                .ForMember(dst => dst.Id, cfg => cfg.ResolveUsing(x => "beercategories/" + x.id))
                .ForMember(dst => dst.Name, cfg => cfg.MapFrom(src => src.cat_name))
                .ForMember(dst => dst.LastChanged, cfg => cfg.MapFrom(src => src.last_mod));
        }
    }
}