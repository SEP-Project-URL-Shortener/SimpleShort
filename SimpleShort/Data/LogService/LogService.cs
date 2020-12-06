/*
 * LogService:
 * Responsible for creating a log object, adding it to the database, and printing it to the screen.
 */

using Newtonsoft.Json;
using SimpleShort.Entities.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SimpleShort.Data.LogService
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _repository;

        public LogService(ILogRepository repository)
            => _repository = repository;

        public async Task<bool> Log(string location, string request, string information)
        {
            var log = new LogEntity(location, request, information);
            if (!await _repository.CreateLog(log))
                Debug.WriteLine(" -- LOG NOT CREATED -- ");
            Debug.WriteLine(JsonConvert.SerializeObject(log));
            return true;
        }
    }
}