using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IgCloneMono.Api.Templates;
using StackExchange.Redis;

namespace IgCloneMono.Api.Repositories
{
    public class FollowerListRedisRepository : RedisTemplate<Dictionary<long, bool>>, IFollowerListRepository
    {
        public FollowerListRedisRepository(IDatabase redisDb) : base(redisDb)
        {
        }

        protected override string GetPrefix()
        {
            return "FOLLOWERLIST";
        }

        protected override TimeSpan GetLifeTime()
        {
            return TimeSpan.FromMinutes(10);
        }

        public async Task<Dictionary<long, bool>> GetCachedFollowerList(long playerId)
        {
            return await GetById(playerId.ToString());
        }

        public async Task StoreCacheFollowerList(Dictionary<long, bool> followerList, long playerId)
        {
            await SaveById(playerId.ToString(), followerList);
        }
    }
}