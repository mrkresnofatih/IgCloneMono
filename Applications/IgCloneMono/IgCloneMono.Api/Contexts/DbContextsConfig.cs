using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace IgCloneMono.Api.Contexts
{
    [ExcludeFromCodeCoverage]
    public static class DbContextsConfig
    {
        public static void AddAppDbContexts(this IServiceCollection service)
        {
            service.AddIgCloneDbContext();
            service.AddRedisService();
        }
    }
}