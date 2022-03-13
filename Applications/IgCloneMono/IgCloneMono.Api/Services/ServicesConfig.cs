using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace IgCloneMono.Api.Services
{
    public static class ServicesConfig
    {
        [ExcludeFromCodeCoverage]
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<AuthService>();
            services.AddScoped<ConnectionService>();
        }
    }
}