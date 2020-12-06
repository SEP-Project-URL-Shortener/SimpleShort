/*
 * IShortenUrlRepository:
 * Responsible for giving the user an interface of the repository instead of giving them the entire repository.
 */

using SimpleShort.Entities;
using SimpleShort.Models;
using System.Threading.Tasks;

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