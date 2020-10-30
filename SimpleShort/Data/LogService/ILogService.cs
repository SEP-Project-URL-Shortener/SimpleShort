using System.Threading.Tasks;

namespace SimpleShort.Data.LogService
{
    public interface ILogService
    {
        Task<bool> Log(string location, string request, string information);
    }
}
