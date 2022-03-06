using System;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace IgCloneMono.Api.Contexts
{
    public static class RedisMultiplexerContext
    {
        private static ConnectionMultiplexer CreateConnectionMultiplexer()
        {
            var igCloneRedisHost = Environment.GetEnvironmentVariable("IGCLONEREDIS_HOST");
            var igCloneRedisPort = Environment.GetEnvironmentVariable("IGCLONEREDIS_PORT");
            var connectionString = $"{igCloneRedisHost}:{igCloneRedisPort},abortConnect=false";
            return ConnectionMultiplexer.Connect(connectionString);
        }

        public static void AddRedisService(this IServiceCollection services)
        {
            var redis = CreateConnectionMultiplexer();
            services.AddSingleton(redis.GetDatabase());
        }
    }
}