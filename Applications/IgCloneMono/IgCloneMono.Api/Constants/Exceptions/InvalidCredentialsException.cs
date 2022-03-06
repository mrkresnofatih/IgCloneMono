using System;

namespace IgCloneMono.Api.Constants.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException() : base(ErrorCodes.INVALID_CREDENTIALS)
        {
        }
    }
}