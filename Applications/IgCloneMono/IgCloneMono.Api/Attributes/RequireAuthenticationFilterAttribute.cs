using System;
using System.Threading.Tasks;
using IgCloneMono.Api.Constants;
using IgCloneMono.Api.Constants.Exceptions;
using IgCloneMono.Api.Repositories;
using IgCloneMono.Api.Utils;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IgCloneMono.Api.Attributes
{
    public class RequireAuthenticationFilterAttribute : Attribute, IAsyncActionFilter
    {
        public RequireAuthenticationFilterAttribute(PlayerAccessTokenUtils playerAccessTokenUtils, PlayerAccessTokenRepository playerAccessTokenRepository)
        {
            _playerAccessTokenUtils = playerAccessTokenUtils;
            _playerAccessTokenRepository = playerAccessTokenRepository;
        }

        private readonly PlayerAccessTokenRepository _playerAccessTokenRepository;
        private readonly PlayerAccessTokenUtils _playerAccessTokenUtils;
        
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var tokenHeaderExists = context
                .HttpContext
                .Request
                .Headers
                .ContainsKey(CustomHeaders.IGCLONE_PLAYER_TOKEN);
            if (!tokenHeaderExists)
            {
                throw new InvalidCredentialsException();
            }

            var token = context.HttpContext.Request.Headers[CustomHeaders.IGCLONE_PLAYER_TOKEN];
            var isTokenValid = _playerAccessTokenUtils.ValidateToken(token);

            if (!isTokenValid)
            {
                throw new InvalidCredentialsException();
            }

            var playerIdFromToken = _playerAccessTokenUtils.GetPlayerIdFromToken(token);
            var tokenFromRedis = await _playerAccessTokenRepository.RetrieveStoredAccessToken(playerIdFromToken.ToString());
            if (tokenFromRedis == null)
            {
                throw new InvalidCredentialsException();
            }

            var extendTokenTask = _playerAccessTokenRepository.SaveOrExtendAccessToken(playerIdFromToken.ToString(), token);
            var goToController = Task.Run(() => next());
            Task.WaitAll(extendTokenTask, goToController);
        }
    }
}