using System.Collections.Generic;
using System.Threading.Tasks;
using IgCloneMono.Api.Models;

namespace IgCloneMono.Api.Repositories
{
    public interface IConnectionDbRepository
    {
        Task<Connection> AddOne(Connection connection);

        Task<Connection> GetOneByPlayerIdAndFollowerId(long playerId, long followerId);

        Task<Connection> DeleteOneByConnectionId(long connectionId);

        Task<Connection> UndeleteOneByConnectionId(long connectionId);

        Task<Dictionary<long, bool>> GetFollowerIdList(long playerId);

        Task<Dictionary<long, bool>> GetFollowingIdList(long playerId);
    }
}