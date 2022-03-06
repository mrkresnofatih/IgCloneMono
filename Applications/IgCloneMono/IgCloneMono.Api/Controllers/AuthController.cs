using System.Threading.Tasks;
using IgCloneMono.Api.Attributes;
using IgCloneMono.Api.Models;
using IgCloneMono.Api.Services;
using IgCloneMono.Api.Utils;
using Microsoft.AspNetCore.Mvc;

namespace IgCloneMono.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        private readonly AuthService _authService;

        [HttpPost("register")]
        public async Task<ResponsePayload<PlayerGetDto>> Register(
            [FromBody] PlayerCreateUpdateDto playerCreateUpdateDto)
        {
            var createdPlayer = await _authService.Register(playerCreateUpdateDto);
            return ResponseHandler.WrapSuccess(createdPlayer);
        }

        [HttpPost("login")]
        public async Task<ResponsePayload<string>> Login([FromBody] PlayerLoginDto playerLoginDto)
        {
            var token = await _authService.Login(playerLoginDto);
            return ResponseHandler.WrapSuccess(token);
        }

        [HttpGet("helloworld")]
        [TypeFilter(typeof(RequireAuthenticationFilterAttribute))]
        public ResponsePayload<string> HelloWorld()
        {
            return ResponseHandler.WrapSuccess("sukses");
        }
    }
}