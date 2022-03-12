using System;
using System.Threading.Tasks;
using IgCloneMono.Api.Templates;
using StackExchange.Redis;

namespace IgCloneMono.Api.Repositories
{
    public class PlayerAccessTokenRepository : RedisTemplate<string>, IPlayerAccessTokenRepository
    {
        public PlayerAccessTokenRepository(IDatabase redisDb) : base(redisDb)
        {
        }

        protected override string GetPrefix()
        {
            return "PLAYERACCESSTOKEN";
        }

        protected override TimeSpan GetLifeTime()
        {
            return TimeSpan.FromHours(2);
        }

        public async Task SaveOrExtendAccessToken(string id, string token)
        {
            await SaveById(id, token);
        }

        public async Task<string> RetrieveStoredAccessToken(string id)
        {
            return await GetById(id);
        }

        public async Task RevokeAccessToken(string id)
        {
            await DeleteById(id);
        }
    }
}