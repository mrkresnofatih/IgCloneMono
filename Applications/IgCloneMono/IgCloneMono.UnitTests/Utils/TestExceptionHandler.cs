using System.IO;
using FluentAssertions;
using IgCloneMono.Api.Constants;
using IgCloneMono.Api.Constants.Exceptions;
using IgCloneMono.Api.Utils;
using Xunit;

namespace IgCloneMono.UnitTests.Utils
{
    public class TestExceptionHandler
    {
        [Fact]
        public void ItShouldReturnBadRequestCodeOnRecordNotFound()
        {
            var error = new RecordNotFoundException();
            var returnErrorCode = ExceptionHandler.GetErrorCode(error);
            returnErrorCode.Should().Be(ErrorCodes.BAD_REQUEST);
        }

        [Fact]
        public void ItShouldReturnInvalidCredentialsCodeOnInvalidCredentials()
        {
            var error = new InvalidCredentialsException();
            var returnErrorCode = ExceptionHandler.GetErrorCode(error);
            returnErrorCode.Should().Be(ErrorCodes.INVALID_CREDENTIALS);
        }

        [Fact]
        public void ItShouldReturnUnhandledCodeOnUnspecifiedExceptionClasses()
        {
            var error = new InvalidDataException();
            var returnErrorCode = ExceptionHandler.GetErrorCode(error);
            returnErrorCode.Should().Be(ErrorCodes.UNHANDLED);
        }
    }
}