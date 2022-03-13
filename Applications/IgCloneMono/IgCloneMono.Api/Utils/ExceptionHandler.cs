using System;
using System.Diagnostics.CodeAnalysis;
using IgCloneMono.Api.Constants;
using IgCloneMono.Api.Constants.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace IgCloneMono.Api.Utils
{
    public static class ExceptionHandler
    {
        [ExcludeFromCodeCoverage]
        public static void UseAppExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var exception = context
                        .Features
                        .Get<IExceptionHandlerPathFeature>()
                        .Error;
                    var errorCode = GetErrorCode(exception);
                    context.Response.StatusCode = 200;
                    await context.Response.WriteAsJsonAsync(ResponseHandler.WrapFailure<object>(errorCode));
                });
            });
        }

        public static string GetErrorCode(Exception exception)
        {
            switch (exception)
            {
                case UsernameTakenException:
                    return ErrorCodes.USERNAME_TAKEN;
                case InvalidCredentialsException:
                    return ErrorCodes.INVALID_CREDENTIALS;
                case BadRequestException:
                case RecordNotFoundException:
                    return ErrorCodes.BAD_REQUEST;
                default:
                    return ErrorCodes.UNHANDLED;
            }
        }
    }
}