/*
 * ILogService:
 * Responsible for giving the user an interface of the service instead of giving them the entire service.
 */

// Included Libraries

using System.Threading.Tasks;

namespace SimpleShort.Interfaces
{
    public interface ILogService
    {
        Task<bool> Log(string location, string request, string information);
    }
}