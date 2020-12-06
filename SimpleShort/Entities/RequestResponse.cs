/*
 * RequestResponse:
 * The object used for sending a response back to the caller. I wish I could make some properties private to encapsulate it further from the user but with EF this is not possible.
 *
 * Status: string = The status of the request.
 * Message: string = Any information pertaining to the request.
 */

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