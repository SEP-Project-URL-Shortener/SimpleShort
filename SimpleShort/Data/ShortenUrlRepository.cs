using System;
using System.Linq;
using System.Threading.Tasks;
using Compute.Classes.Hashing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimpleShort.Entities;
using SimpleShort.Models;

namespace SimpleShort.Data
{
    public class ShortenUrlRepository : IShortenUrlRepository
    {
        private readonly ShortenedUrlContext _context;
        private readonly IConfiguration _configuration;

        public ShortenUrlRepository(ShortenedUrlContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> GetLongUrl(string path)
        {
            var shortenedUrl = await _context.ShortenedUrls.Where(shortUrl => shortUrl.Path.Equals(path))
                .FirstOrDefaultAsync();

            if (shortenedUrl == null)
                return null;

            var expiration = DateTime.Parse(shortenedUrl.Expiration);

            if (DateTime.Compare(expiration, DateTime.UtcNow) != 1)
                return null;

            shortenedUrl.UseShortUrl();
            await SaveChanges();

            return shortenedUrl.OriginalUrl;
        }

        public async Task<SimpleShortUrl> GetShortenedUrl(string path)
        {
            return await _context.ShortenedUrls.Where(shortUrl => shortUrl.Path.Equals(path))
                .Select(idvShortenedUrl => new SimpleShortUrl(idvShortenedUrl))
                .Where(shortUrl => DateTime.Compare(shortUrl.Expiration, DateTime.UtcNow) == 1)
                .FirstOrDefaultAsync();
        }


        public async Task<SimpleShortUrl[]> GetAllShortenedUrls(string ipAddress)
        {
            var sha512 = new Sha512();
            var hashedIpAddress = sha512.Hash(ipAddress, _configuration["Hashing:Pepper"]);

            return await _context.ShortenedUrls
                .Where(shortUrl => shortUrl.IpAddress.Equals(hashedIpAddress))
                .Select(idvShortenedUrl => new SimpleShortUrl(idvShortenedUrl))
                .ToArrayAsync();
        }

        public async Task<SimpleShortUrl> CreateShortedUrl(CreateShortenedUrlModel model)
        {
            if (await IsPathTaken(model.Path))
                return null;

            var hashedIpAddress = HashIpAddress(model.IpAddress);
            var shortenedUrl = new ShortenedUrl(hashedIpAddress, model.OriginalUrl, model.Path, model.Expiration);

            await _context.ShortenedUrls.AddAsync(shortenedUrl);

            return await SaveChanges()
                ? new SimpleShortUrl(shortenedUrl)
                : null;
        }

        public async Task<SimpleShortUrl> UpdateShortedUrl(UpdateShortenedUrlModel model)
        {
            var hashedIpAddress = HashIpAddress(model.IpAddress);

            var existingUrl = await _context.ShortenedUrls
                .Where(shortUrl => shortUrl.IpAddress == hashedIpAddress)
                .FirstOrDefaultAsync();

            if (existingUrl == null)
                return null;

            existingUrl.Path = model.Path;
            existingUrl.OriginalUrl = model.OriginalUrl;
            existingUrl.Expiration = model.Expiration;

            _context.ShortenedUrls.Update(existingUrl);

            return await SaveChanges()
                ? new SimpleShortUrl(existingUrl)
                : null;
        }

        public async Task<RequestResponse> DeleteShortedUrl(DeleteShortenedUrlModel model)
        {
            var hashedIpAddress = HashIpAddress(model.IpAddress);

            var existingUrl = await _context.ShortenedUrls
                .Where(shortUrl => shortUrl.IpAddress == hashedIpAddress)
                .FirstOrDefaultAsync();

            _context.ShortenedUrls.Remove(existingUrl);

            return await SaveChanges()
                ? new RequestResponse("Success", "Url has been deleted")
                : new RequestResponse("Failed", "Error while deleting url");
        }

        private string HashIpAddress(string ipAddress)
        {
            var sha512 = new Sha512();
            return sha512.Hash(ipAddress, _configuration["Hashing:Pepper"]);
        }

        private async Task<bool> IsPathTaken(string path)
            => await _context.ShortenedUrls.AnyAsync(shortUrl => shortUrl.Path.Equals(path));// TODO: && shortUrl.IsExpired());

        private async Task<bool> SaveChanges()
            => await _context.SaveChangesAsync() > 0;
    }
}
