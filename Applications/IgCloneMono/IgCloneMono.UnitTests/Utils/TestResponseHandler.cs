using FluentAssertions;
using IgCloneMono.Api.Utils;
using Xunit;

namespace IgCloneMono.UnitTests.Utils
{
    public class TestResponseHandler
    {
        [Fact]
        public void ItShouldReturnDataNullErrorNonNullOnError()
        {
            var error = "error";
            var responsePayload = ResponseHandler.WrapFailure<string>(error);

            responsePayload.Data.Should().BeNull();
            responsePayload.Error.Should().Be(error);
        }

        [Fact]
        public void ItShouldReturnDataNonNullErrorNullOnSuccess()
        {
            var data = "someData";
            var responsePayload = ResponseHandler.WrapSuccess(data);
            
            responsePayload.Data.Should().Be(data);
            responsePayload.Error.Should().BeNull();
        }
    }
}