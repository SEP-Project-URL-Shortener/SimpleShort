using System;

namespace SimpleShort.Entities.Logging
{
    public class LogEntity
    {
        public Guid Id { get; set; }
        public DateTime Occurred { get; set; }
        public string Location { get; set; }
        public string Request { get; set; }
        public string Information { get; set; }

        public LogEntity(string location, string request, string information)
        {
            Id = Guid.NewGuid();
            Occurred = DateTime.UtcNow;
            Location = location;
            Request = request;
            Information = information;
        }
    }
}
