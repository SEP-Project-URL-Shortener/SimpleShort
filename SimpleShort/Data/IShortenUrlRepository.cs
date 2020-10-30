using System.Threading.Tasks;
using SimpleShort.Entities;
using SimpleShort.Models;

namespace SimpleShort.Data
{
    public interface IShortenUrlRepository
    {
        Task<string> GetLongUrl(string path);
        Task<SimpleShortUrl> GetShortenedUrl(string path);
        Task<SimpleShortUrl[]> GetAllShortenedUrls(string ipAddress);
        Task<SimpleShortUrl> CreateShortedUrl(CreateShortenedUrlModel model);
        Task<SimpleShortUrl> UpdateShortedUrl(UpdateShortenedUrlModel model);
        Task<RequestResponse> DeleteShortedUrl(DeleteShortenedUrlModel model);
    }
}
