using System.Collections.Generic;
using System.Threading.Tasks;
using IgCloneMono.Api.Attributes;
using IgCloneMono.Api.Constants;
using IgCloneMono.Api.Models;
using IgCloneMono.Api.Services;
using IgCloneMono.Api.Utils;
using Microsoft.AspNetCore.Mvc;

namespace IgCloneMono.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConnectionController  : ControllerBase, IConnectionController
    {
        public ConnectionController(ConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        private readonly ConnectionService _connectionService;
        
        [HttpGet("follow/{candidateId}")]
        [TypeFilter(typeof(RequireAuthenticationFilterAttribute))]
        public async Task<ResponsePayload<Connection>> Follow(long candidateId, [FromHeader(Name = CustomHeaders.IGCLONE_PLAYER_TOKEN)] string token)
        {
            var res = await _connectionService.Follow(candidateId, token);
            return ResponseHandler.WrapSuccess(res);
        }

        [HttpGet("unfollow/{candidateId}")]
        [TypeFilter(typeof(RequireAuthenticationFilterAttribute))]
        public async Task<ResponsePayload<Connection>> Unfollow(long candidateId, [FromHeader(Name = CustomHeaders.IGCLONE_PLAYER_TOKEN)] string token)
        {
            var res = await _connectionService.Unfollow(candidateId, token);
            return ResponseHandler.WrapSuccess(res);
        }

        [HttpGet("myFollowings")]
        [TypeFilter(typeof(RequireAuthenticationFilterAttribute))]
        public async Task<ResponsePayload<Dictionary<long, PlayerGetDto>>> GetMyFollowingList([FromHeader(Name = CustomHeaders.IGCLONE_PLAYER_TOKEN)] string token, [FromQuery] int page)
        {
            var res = await _connectionService.GetFollowings(token, page);
            return ResponseHandler.WrapSuccess(res);
        }

        [HttpGet("myFollowers")]
        [TypeFilter(typeof(RequireAuthenticationFilterAttribute))]
        public async Task<ResponsePayload<Dictionary<long, PlayerGetDto>>> GetMyFollowerList([FromHeader(Name = CustomHeaders.IGCLONE_PLAYER_TOKEN)] string token, [FromQuery] int page)
        {
            var res = await _connectionService.GetFollowers(token, page);
            return ResponseHandler.WrapSuccess(res);
        }
    }
}