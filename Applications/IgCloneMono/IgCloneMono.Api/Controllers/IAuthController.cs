using System.Threading.Tasks;
using IgCloneMono.Api.Models;
using IgCloneMono.Api.Utils;

namespace IgCloneMono.Api.Controllers
{
    public interface IAuthController
    {
        Task<ResponsePayload<string>> Login(PlayerLoginDto playerLoginDto);

        Task<ResponsePayload<PlayerGetDto>> Register(PlayerCreateUpdateDto playerCreateUpdateDto);
    }
}