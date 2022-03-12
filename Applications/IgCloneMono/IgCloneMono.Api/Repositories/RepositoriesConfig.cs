using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace IgCloneMono.Api.Repositories
{
    public static class RepositoriesConfig
    {
        [ExcludeFromCodeCoverage]
        public static void AddAppRepositories(this IServiceCollection services)
        {
            services.AddScoped<PlayerDbRepository>();
            services.AddSingleton<PlayerAccessTokenRepository>();
            services.AddScoped<ConnectionDbRepository>();
            services.AddSingleton<FollowerListRedisRepository>();
            services.AddSingleton<FollowingListRedisRepository>();
        }
    }
}