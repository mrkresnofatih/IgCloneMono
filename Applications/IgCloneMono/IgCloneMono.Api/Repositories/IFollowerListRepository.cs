using System.Collections.Generic;
using System.Threading.Tasks;

namespace IgCloneMono.Api.Repositories
{
    public interface IFollowerListRepository
    {
        Task<Dictionary<long, bool>> GetCachedFollowerList(long playerId);

        Task StoreCacheFollowerList(Dictionary<long, bool> followerList, long playerId);
    }
}