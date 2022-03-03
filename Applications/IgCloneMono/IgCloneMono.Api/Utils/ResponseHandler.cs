namespace IgCloneMono.Api.Utils
{
    public class ResponseHandler
    {
        public static ResponsePayload<T> WrapSuccess<T>(T data)
        {
            return new ResponsePayload<T> {Data = data, Error = null};
        }

        public static ResponsePayload<T> WrapFailure<T>(string error)
        {
            return new ResponsePayload<T> {Error = error};
        }
    }

    public class ResponsePayload<T>
    {
        public T Data { get; set; }
        public string Error { get; set; }
    }
}