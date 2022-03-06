using IgCloneMono.Api.Constants;
using IgCloneMono.Api.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IgCloneMono.Api.Controllers
{
    public static class ControllersConfig
    {
        public static void AddAppControllers(this IServiceCollection services)
        {
            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var logger = context.HttpContext.RequestServices
                        .GetRequiredService<ILogger<Startup>>();
                    logger.LogInformation("Invalid ModelState");
                    return new OkObjectResult(ResponseHandler.WrapFailure<object>(ErrorCodes.BAD_REQUEST));
                };
            });
        }
    }
}