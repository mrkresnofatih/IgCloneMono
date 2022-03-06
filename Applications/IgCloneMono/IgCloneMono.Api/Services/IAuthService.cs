using System.Threading.Tasks;
using IgCloneMono.Api.Models;

namespace IgCloneMono.Api.Services
{
    public interface IAuthService
    {
        public Task<string> Login(PlayerLoginDto playerLoginDto);

        public Task<PlayerGetDto> Register(PlayerCreateUpdateDto playerCreateUpdateDto);
    }
}