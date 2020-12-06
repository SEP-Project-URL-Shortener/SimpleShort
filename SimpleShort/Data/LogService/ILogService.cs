/*
 * ILogService:
 * Responsible for giving the user an interface of the service instead of giving them the entire service.
 */

using System.Threading.Tasks;

namespace SimpleShort.Data.LogService
{
    public interface ILogService
    {
        Task<bool> Log(string location, string request, string information);
    }
}