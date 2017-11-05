﻿using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ImportBeerDBTemplate;
using ImportBeerDBTemplate.CsvRowEntities;
using Raven.Client.Documents.Operations;

namespace MyImportBeerDB.RavenEntities
{
    public class Hops
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Origin { get; set; }
        public double AlphaLow { get; set; }
        public double AlphaHigh { get; set; }
        public string Notes { get; set; }
        public Dictionary<string,string> SubstituteIdByName { get; set; }
    }

    public class HopsMappingProfile : Profile
    {
        public HopsMappingProfile()
        {
            CreateMap<HopsRow, Hops>()
                .ForMember(dst => dst.Id, cfg => cfg.ResolveUsing(x => "hops/" + x._id))
                .ForMember(dst => dst.AlphaLow, cfg => cfg.MapFrom(src => src.alpha_low))
                .ForMember(dst => dst.AlphaHigh, cfg => cfg.MapFrom(src => src.alpha_high))
                .ForMember(dst => dst.SubstituteIdByName, cfg => cfg.ResolveUsing(h =>
                {
                    var results = new Dictionary<string, string>();
                    var substitutes = InMemoryOpenBeerDataDB.HopSubstitutes.Where(s => s.hop_id == h._id);
                    foreach (var hopSubstitute in substitutes)
                    {
                        var substituteHop = InMemoryOpenBeerDataDB.Hops
                                                .FirstOrDefault(x => x._id == hopSubstitute.substitute_id);
                        if (substituteHop != null)
                            results.Add(substituteHop.name, "hops/" + substituteHop._id);
                    }

                    return results;
                }));
        }
    }
}