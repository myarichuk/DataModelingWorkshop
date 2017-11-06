using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ImportCsvData.CsvRowEntities;

namespace ImportCsvData.RavenEntities
{
    public class HopsMappingProfile : Profile
    {
        public HopsMappingProfile()
        {
            CreateMap<HopRow, Hop>()
                .ForMember(dst => dst.Id, cfg => cfg.ResolveUsing(x => "hops/" + x._id))
                .ForMember(dst => dst.AlphaLow, cfg => cfg.MapFrom(src => src.alpha_low))
                .ForMember(dst => dst.AlphaHigh, cfg => cfg.MapFrom(src => src.alpha_high))
                .ForMember(dst => dst.SubstituteIdByName, cfg => cfg.ResolveUsing(h =>
                {
                    var results = new Dictionary<string, string>();
                    var substitutes = InMemoryOpenBeerIngredientsDB.HopSubstitutes.Where(s => s.hop_id == h._id);
                    foreach (var hopSubstitute in substitutes)
                    {
                        var substituteHop = InMemoryOpenBeerIngredientsDB.Hops
                            .FirstOrDefault(x => x._id == hopSubstitute.substitute_id);
                        if (substituteHop != null)
                            results.Add(substituteHop.name, "hops/" + substituteHop._id);
                    }

                    return results;
                }));
        }
    }
}