using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ImportCsvData.CsvRowEntities;

namespace ImportCsvData.RavenEntities
{
    public class Episode
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string OriginalAirDate { get; set; }
        public string ProductionCode { get; set; }
        public int Season { get; set; }
        public double NumberInSeason { get; set; }
        public double NumberInSeries { get; set; }
        public double UsViewersInMillions { get; set; }
        public double Views { get; set; }
        public double ImdbRating { get; set; }
        public double ImdbVotes { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
    }

    public class EpisodeMappingProfile : Profile
    {
        public EpisodeMappingProfile()
        {
            CreateMap<SimpsonsEpisodeRow, Episode>()
                .ForMember(dst => dst.Id, cfg => cfg.ResolveUsing(x => "episodes/" + x.id))
                .ForMember(dst => dst.OriginalAirDate, cfg => cfg.MapFrom(x => x.original_air_date))
                .ForMember(dst => dst.ProductionCode, cfg => cfg.MapFrom(x => x.production_code))
                .ForMember(dst => dst.NumberInSeason, cfg => cfg.MapFrom(x => x.number_in_season))
                .ForMember(dst => dst.NumberInSeries, cfg => cfg.MapFrom(x => x.number_in_series))
                .ForMember(dst => dst.ImdbRating, cfg => cfg.MapFrom(x => x.imdb_rating))
                .ForMember(dst => dst.ImdbVotes, cfg => cfg.MapFrom(x => x.imdb_votes))
                .ForMember(dst => dst.ImageUrl, cfg => cfg.MapFrom(x => x.image_url))
                .ForMember(dst => dst.VideoUrl, cfg => cfg.MapFrom(x => x.video_url))
                .ForMember(dst => dst.UsViewersInMillions, cfg => cfg.MapFrom(x => x.us_viewers_in_millions));
        }
    }
}
