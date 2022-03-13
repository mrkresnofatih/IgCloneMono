using System;

namespace IgCloneMono.Api.Constants.Exceptions
{
    public class UsernameTakenException : Exception
    {
        public UsernameTakenException() : base(ErrorCodes.USERNAME_TAKEN)
        {
        }
    }
}