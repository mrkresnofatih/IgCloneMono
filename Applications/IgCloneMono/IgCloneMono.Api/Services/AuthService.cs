using System.Threading.Tasks;
using AutoMapper;
using IgCloneMono.Api.Constants.Exceptions;
using IgCloneMono.Api.Models;
using IgCloneMono.Api.Repositories;
using IgCloneMono.Api.Utils;

namespace IgCloneMono.Api.Services
{
    public class AuthService : IAuthService
    {
        public AuthService(PlayerDbRepository playerDbRepository, 
            IMapper mapper, PlayerPasswordUtils playerPasswordUtils, 
            PlayerAccessTokenUtils playerAccessTokenUtils, 
            PlayerAccessTokenRepository playerAccessTokenRepository)
        {
            _mapper = mapper;
            _playerDbRepository = playerDbRepository;
            _playerPasswordUtils = playerPasswordUtils;
            _playerAccessTokenUtils = playerAccessTokenUtils;
            _playerAccessTokenRepository = playerAccessTokenRepository;
        }

        private readonly PlayerAccessTokenRepository _playerAccessTokenRepository;
        private readonly PlayerAccessTokenUtils _playerAccessTokenUtils;
        private readonly IMapper _mapper;
        private readonly PlayerPasswordUtils _playerPasswordUtils;
        private readonly PlayerDbRepository _playerDbRepository;
        
        public async Task<string> Login(PlayerLoginDto playerLoginDto)
        {
            var foundPlayer = await _playerDbRepository.GetRawOneByUsername(playerLoginDto.Username);
            var isPasswordCorrect = _playerPasswordUtils.IsCompareValid(playerLoginDto.Password, foundPlayer.Password);
            if (!isPasswordCorrect)
            {
                throw new InvalidCredentialsException();
            }

            var token = _playerAccessTokenUtils.GenerateToken(foundPlayer.PlayerId);
            await _playerAccessTokenRepository.SaveOrExtendAccessToken(foundPlayer.PlayerId.ToString(), token);
            return token;
        }

        public async Task<PlayerGetDto> Register(PlayerCreateUpdateDto playerCreateUpdateDto)
        {
            var existingPlayerWithSameUsername =
                await _playerDbRepository.GetRawOneOrDefault(playerCreateUpdateDto.Username);
            if (existingPlayerWithSameUsername != null)
            {
                throw new UsernameTakenException();
            }
            
            var newPlayer = _mapper.Map<Player>(playerCreateUpdateDto);
            newPlayer.Banned = false;
            newPlayer.Password = _playerPasswordUtils.DoHash(playerCreateUpdateDto.Password);
            newPlayer.Deleted = false;
            newPlayer.ImageUrl =
                "https://source.boringavatars.com/bauhaus/120/Henrietta%20Swan?colors=FFAD08,EDD75A,73B06F,0C8F8F,405059";
            return await _playerDbRepository.AddOne(newPlayer);
        }
    }
}