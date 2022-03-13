using System.Collections.Generic;
using System.Threading.Tasks;
using IgCloneMono.Api.Models;

namespace IgCloneMono.Api.Services
{
    public interface IConnectionService
    {
        Task<Connection> Follow(long candidateId, string token);

        Task<Connection> Unfollow(long candidateId, string token);

        Task<Dictionary<long, PlayerGetDto>> GetFollowers(string token, int page);

        Task<Dictionary<long, PlayerGetDto>> GetFollowings(string token, int page);

        Task<Dictionary<long, bool>> GetFollowerIdListRedisOrDb(string token);
        
        Task<Dictionary<long, bool>> GetFollowingIdListRedisOrDb(string token);
    }
}