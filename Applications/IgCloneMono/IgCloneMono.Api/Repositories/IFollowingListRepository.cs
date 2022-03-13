using System.Collections.Generic;
using System.Threading.Tasks;

namespace IgCloneMono.Api.Repositories
{
    public interface IFollowingListRepository
    {
        Task<Dictionary<long, bool>> GetCachedFollowingList(long playerId);

        Task StoreCacheFollowingList(Dictionary<long, bool> followingList, long playerId);

        Task StoreOneToCacheFollowingList(long candidateId, long playerId);

        Task RemoveOneFromCacheFollowingList(long candidateId, long playerId);
    }
}