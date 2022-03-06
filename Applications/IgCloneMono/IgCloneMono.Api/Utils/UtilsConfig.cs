using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace IgCloneMono.Api.Utils
{
    public static class UtilsConfig
    {
        [ExcludeFromCodeCoverage]
        public static void AddAppUtils(this IServiceCollection services)
        {
            services.AddSingleton<PlayerPasswordUtils>();
            services.AddSingleton<PlayerAccessTokenUtils>();
            services.AddAutomapperConfig();
        }
    }
}