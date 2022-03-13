using System.Collections.Generic;
using System.Threading.Tasks;
using IgCloneMono.Api.Models;
using IgCloneMono.Api.Utils;

namespace IgCloneMono.Api.Controllers
{
    public interface IConnectionController
    {
        Task<ResponsePayload<Connection>> Follow(long candidateId, string token);

        Task<ResponsePayload<Connection>> Unfollow(long candidateId, string token);

        Task<ResponsePayload<Dictionary<long, PlayerGetDto>>> GetMyFollowingList(string token, int page);
        
        Task<ResponsePayload<Dictionary<long, PlayerGetDto>>> GetMyFollowerList(string token, int page);
    }
}