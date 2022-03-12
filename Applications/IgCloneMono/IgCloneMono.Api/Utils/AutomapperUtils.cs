using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using IgCloneMono.Api.Models;
using Microsoft.Extensions.DependencyInjection;

namespace IgCloneMono.Api.Utils
{
    public static class AutomapperUtils
    {
        public static IMapper CreateIMapper()
        {
            var mapperProfile = new MapperConfiguration(mp => mp.AddProfile(new AutomapperProfile()));
            return mapperProfile.CreateMapper();
        }

        [ExcludeFromCodeCoverage]
        public static void AddAutomapperConfig(this IServiceCollection services)
        {
            services.AddSingleton(CreateIMapper());
        }
    }

    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            // Player
            CreateMap<Player, PlayerGetDto>().ReverseMap();
            CreateMap<Player, PlayerCreateUpdateDto>().ReverseMap();
        }
    }
}