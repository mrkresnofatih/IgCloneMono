﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IgCloneMono.Api.Templates;
using StackExchange.Redis;

namespace IgCloneMono.Api.Repositories
{
    public class FollowingListRedisRepository : RedisTemplate<Dictionary<long, bool>>, IFollowingListRepository
    {
        public FollowingListRedisRepository(IDatabase redisDb) : base(redisDb)
        {
        }

        protected override string GetPrefix()
        {
            return "FOLLOWINGLIST";
        }

        protected override TimeSpan GetLifeTime()
        {
            return TimeSpan.FromMinutes(10);
        }

        public async Task<Dictionary<long, bool>> GetCachedFollowingList(long playerId)
        {
            return await GetById(playerId.ToString());
        }

        public async Task StoreCacheFollowingList(Dictionary<long, bool> followingList, long playerId)
        {
            await SaveById(playerId.ToString(), followingList);
        }
    }
}