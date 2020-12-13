/*
 * LogEntity:
 * The object used for logging. I wish I could make some properties private to encapsulate it further from the user but with EF this is not possible.
 *
 * Id: Guid = Unique Id for log.
 * Occurred: DateTime = When the log occurred.
 * Location: string = Where the log was called from.
 * Request: string = What operation was being done at time of log.
 * Information: string = Any information pertaining to the log.
 */

// Included Libraries
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