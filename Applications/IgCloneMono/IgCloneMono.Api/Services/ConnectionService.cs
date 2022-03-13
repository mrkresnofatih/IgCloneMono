using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IgCloneMono.Api.Constants.Exceptions;
using IgCloneMono.Api.Models;
using IgCloneMono.Api.Repositories;
using IgCloneMono.Api.Utils;

namespace IgCloneMono.Api.Services
{
    public class ConnectionService : IConnectionService
    {
        public ConnectionService(ConnectionDbRepository connectionDbRepository, 
            PlayerAccessTokenUtils playerAccessTokenUtils, FollowerListRedisRepository followerListRedisRepository, 
            FollowingListRedisRepository followingListRedisRepository, PlayerDbRepository playerDbRepository)
        {
            _connectionDbRepository = connectionDbRepository;
            _playerAccessTokenUtils = playerAccessTokenUtils;
            _playerDbRepository = playerDbRepository;
            _followerListRedisRepository = followerListRedisRepository;
            _followingListRedisRepository = followingListRedisRepository;
        }

        private readonly PlayerAccessTokenUtils _playerAccessTokenUtils;
        private readonly ConnectionDbRepository _connectionDbRepository;
        private readonly FollowerListRedisRepository _followerListRedisRepository;
        private readonly FollowingListRedisRepository _followingListRedisRepository;
        private readonly PlayerDbRepository _playerDbRepository;
        
        public async Task<Connection> Follow(long candidateId, string token)
        {
            var playerId = _playerAccessTokenUtils.GetPlayerIdFromToken(token);
            if (playerId == candidateId)
            {
                throw new BadRequestException();
            }

            var candidate = await _playerDbRepository.GetOne(candidateId);
            if (candidate == null)
            {
                throw new BadRequestException();
            }
            
            var connectionFromDb = await _connectionDbRepository.GetOneByPlayerIdAndFollowerId(candidateId, playerId);
            if (connectionFromDb == null)
            {
                var connection = new Connection
                {
                    PlayerId = candidateId,
                    FollowerId = playerId,
                    Deleted = false
                };

                await _followingListRedisRepository.StoreOneToCacheFollowingList(candidateId, playerId);
                await _followerListRedisRepository.StoreOneToCacheFollowerList(playerId, candidateId);
                
                return await _connectionDbRepository.AddOne(connection);
            }

            if (!connectionFromDb.Deleted)
            {
                throw new BadRequestException();
            }

            return await _connectionDbRepository.UndeleteOneByConnectionId(connectionFromDb.ConnectionId);
        }

        public async Task<Connection> Unfollow(long candidateId, string token)
        {
            var playerId = _playerAccessTokenUtils.GetPlayerIdFromToken(token);
            if (playerId == candidateId)
            {
                throw new BadRequestException();
            }

            var candidate = await _playerDbRepository.GetOne(candidateId);
            if (candidate == null)
            {
                throw new BadRequestException();
            }
            
            var connectionFromDb = await _connectionDbRepository.GetOneByPlayerIdAndFollowerId(candidateId, playerId);
            if (connectionFromDb == null)
            {
                throw new RecordNotFoundException();
            }

            if (connectionFromDb.Deleted)
            {
                throw new BadRequestException();
            }

            await _followingListRedisRepository.RemoveOneFromCacheFollowingList(candidateId, playerId);
            await _followerListRedisRepository.RemoveOneFromCacheFollowerList(playerId, candidateId);

            return await _connectionDbRepository.DeleteOneByConnectionId(connectionFromDb.ConnectionId);
        }

        public async Task<Dictionary<long, PlayerGetDto>> GetFollowers(string token, int page)
        {
            var followerList = await GetFollowerIdListRedisOrDb(token);
            if (page == 0) page = 1;
            var limit = 3;
            var skip = (page - 1) * limit;
            var followerListWithPagination = followerList.Keys.Skip(skip).Take(limit).ToList();
            return await _playerDbRepository.GetMany(followerListWithPagination);
        }

        public async Task<Dictionary<long, PlayerGetDto>> GetFollowings(string token, int page)
        {
            var followingList = await GetFollowingIdListRedisOrDb(token);
            if (page == 0) page = 1;
            var limit = 3;
            var skip = (page - 1) * limit;
            var followingListWithPagination = followingList.Keys.Skip(skip).Take(limit).ToList();
            return await _playerDbRepository.GetMany(followingListWithPagination);
        }

        public async Task<Dictionary<long, bool>> GetFollowerIdListRedisOrDb(string token)
        {
            var playerId = _playerAccessTokenUtils.GetPlayerIdFromToken(token);
            var followerIdListFromRedis = await _followerListRedisRepository.GetCachedFollowerList(playerId);
            if (followerIdListFromRedis != null)
            {
                return followerIdListFromRedis;
            }

            var followerIdListFromDb = await _connectionDbRepository.GetFollowerIdList(playerId);
            await _followerListRedisRepository.StoreCacheFollowerList(followerIdListFromDb, playerId);
            return followerIdListFromDb;
        }

        public async Task<Dictionary<long, bool>> GetFollowingIdListRedisOrDb(string token)
        {
            var playerId = _playerAccessTokenUtils.GetPlayerIdFromToken(token);
            var followingIdListFromRedis = await _followingListRedisRepository.GetCachedFollowingList(playerId);
            if (followingIdListFromRedis != null)
            {
                return followingIdListFromRedis;
            }

            var followingIdListFromDb = await _connectionDbRepository.GetFollowingIdList(playerId);
            await _followingListRedisRepository.StoreCacheFollowingList(followingIdListFromDb, playerId);
            return followingIdListFromDb;
        }
    }
}