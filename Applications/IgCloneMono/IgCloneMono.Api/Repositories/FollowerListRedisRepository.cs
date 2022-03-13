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

        public async Task StoreOneToCacheFollowerList(long candidateId, long playerId)
        {
            var followerListFromRedis = await GetCachedFollowerList(playerId);
            if (followerListFromRedis == null) return;
            followerListFromRedis.Add(candidateId, true);
            await StoreCacheFollowerList(followerListFromRedis, playerId);
        }

        public async Task RemoveOneFromCacheFollowerList(long candidateId, long playerId)
        {
            var followerListFromRedis = await GetCachedFollowerList(playerId);
            if (followerListFromRedis == null) return;
            followerListFromRedis.Remove(candidateId);
            await StoreCacheFollowerList(followerListFromRedis, playerId);
        }
    }
}