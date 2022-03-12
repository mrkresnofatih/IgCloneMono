using System;
using System.Diagnostics.CodeAnalysis;
using IgCloneMono.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IgCloneMono.Api.Contexts
{
    [ExcludeFromCodeCoverage]
    public class IgCloneDbContext : DbContext
    {
        public IgCloneDbContext(DbContextOptions<IgCloneDbContext> options) : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
        
        public DbSet<Connection> Connections { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public static class IgCloneDbConfig
    {
        public static void AddIgCloneDbContext(this IServiceCollection services)
        {
            var igCloneDbHost = Environment.GetEnvironmentVariable("IGCLONEDB_HOST");
            var igCloneDbPort = Environment.GetEnvironmentVariable("IGCLONEDB_PORT");
            var igCloneDbUsername = Environment.GetEnvironmentVariable("IGCLONEDB_USERNAME");
            var igCloneDbPassword = Environment.GetEnvironmentVariable("IGCLONEDB_PASSWORD");
            var connectionString = $@"Host={igCloneDbHost};Port={igCloneDbPort};Username={igCloneDbUsername};Password={igCloneDbPassword};Database=IgCloneDb";
            services.AddDbContext<IgCloneDbContext>(opt => opt.UseNpgsql(connectionString));
        }
    }
}