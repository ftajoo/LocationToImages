namespace LocationToImages.WebApi.Models.Wrapper
{
    public class Response<T>
    {
        public Response()
        { }

        public Response(T data)
        {
            Succeeded = true;
            Error = string.Empty;
            Data = data;
        }

        public T Data { get; set; }

        public bool Succeeded { get; set; }

        public string Error { get; set; }
    }
}