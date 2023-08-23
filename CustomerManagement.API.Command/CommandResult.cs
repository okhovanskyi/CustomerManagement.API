using System.Net;

namespace CustomerManagement.API.Command
{
    public class CommandResult
    {
        public HttpStatusCode HttpStatusCode { get; set; }

        public string? Message { get; set; }

        public AggregateException? AggregateException { get; set; }
    }
}
