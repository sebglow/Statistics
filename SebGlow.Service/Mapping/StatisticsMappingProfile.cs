using SebGlow.Service.Model;
using AutoMapper;
using DataAccess;

namespace SebGlow.Service.Mapping
{
    public class StatisticsMappingProfile : Profile
    {
        public StatisticsMappingProfile()
        {
            CreateMap<Statistic, GitHubStatistics>()
                .ForMember(dest => dest.avgForks, source => source.MapFrom(src => src.avgForks))
                .ForMember(dest => dest.avgSize, source => source.MapFrom(src => src.avgSize))
                .ForMember(dest => dest.avgStargazers, source => source.MapFrom(src => src.avgStargazers))
                .ForMember(dest => dest.avgWatchers, source => source.MapFrom(src => src.avgWatchers))
                .ForMember(dest => dest.owner, source => source.MapFrom(src => src));

            CreateMap<Statistic, RepositoriesOwner>()
                .ForMember(dest => dest.id, source => source.MapFrom(src => src.owner_id))
                .ForMember(dest => dest.login, source => source.MapFrom(src => src.owner_login))
                .ForMember(dest => dest.url, source => source.MapFrom(src => src.owner_url));

            CreateMap<GitHubStatistics, Statistic>()
                .ForMember(dest => dest.avgForks, source => source.MapFrom(src => src.avgForks))
                .ForMember(dest => dest.avgSize, source => source.MapFrom(src => src.avgSize))
                .ForMember(dest => dest.avgStargazers, source => source.MapFrom(src => src.avgStargazers))
                .ForMember(dest => dest.avgWatchers, source => source.MapFrom(src => src.avgWatchers))
                .ForMember(dest => dest.letters, source => source.MapFrom(src => src.letters))
                .ForMember(dest => dest.owner_id, source => source.MapFrom(src => src.owner.id))
                .ForMember(dest => dest.owner_login, source => source.MapFrom(src => src.owner.login))
                .ForMember(dest => dest.owner_url, source => source.MapFrom(src => src.owner.url));

        }
    }
}
