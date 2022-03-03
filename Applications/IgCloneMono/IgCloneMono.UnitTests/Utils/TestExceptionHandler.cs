using System.IO;
using FluentAssertions;
using IgCloneMono.Api.Constants;
using IgCloneMono.Api.Utils;
using Xunit;

namespace IgCloneMono.UnitTests.Utils
{
    public class TestExceptionHandler
    {
        [Fact]
        public void ItShouldReturnBadRequestCodeOnFileNotFound()
        {
            var error = new FileNotFoundException();
            var returnErrorCode = ExceptionHandler.GetErrorCode(error);
            returnErrorCode.Should().Be(ErrorCodes.BAD_REQUEST);
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