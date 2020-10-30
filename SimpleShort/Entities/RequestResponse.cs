namespace SimpleShort.Entities
{
    public class RequestResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }

        public RequestResponse(string status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}
