using System;

namespace IgCloneMono.Api.Constants.Exceptions
{
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException() : base(ErrorCodes.RECORD_NOT_FOUND)
        {
        }
    }
}