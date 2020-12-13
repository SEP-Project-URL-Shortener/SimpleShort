/*
 * LogService:
 * Responsible for creating a log object, adding it to the database, and printing it to the screen.
 */

// Included Libraries
using Newtonsoft.Json;
using SimpleShort.Entities.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using SimpleShort.Interfaces;

namespace SimpleShort.Data.LogService
{
    public class LogService : ILogService
    {
        // Interface of the log repository
        private readonly ILogRepository _repository;

        // Default constructor gets repository via dependency injection
        public LogService(ILogRepository repository)
            => _repository = repository;

        // Create a log and store it into the database
        public async Task<bool> Log(string location, string request, string information)
        {
            // Create a new log entity from the given params
            var log = new LogEntity(location, request, information);

            // Wait until the repository creates the log
            if (!await _repository.CreateLog(log))
                Debug.WriteLine(" -- LOG NOT CREATED -- ");

            // Write to the console
            Debug.WriteLine(JsonConvert.SerializeObject(log));
            return true;
        }
    }
}