using System;

namespace IgCloneMono.Api.Constants.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException() : base(ErrorCodes.BAD_REQUEST)
        {
        }
    }
}