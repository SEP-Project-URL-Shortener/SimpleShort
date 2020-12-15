/*
 * ShortenUrlRepository:
 * Responsible for preforming basic CRUD operations on the database.
 * The user can get the long url, get a certain complete short url object, get all short url objects, create a short url, update a short url, and delete a short url.
 */

// Included Libraries
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimpleShort.Entities;
using SimpleShort.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SimpleShort.Interfaces;

namespace SimpleShort.Data
{
    public class ShortenUrlRepository : IShortenUrlRepository
    {
        // Context to to communicate with the database
        private readonly ShortenedUrlContext _context;

        // Read from the configuration files.
        private readonly IConfiguration _configuration;

        // Default constructor gets context and configuration via dependency injection
        public ShortenUrlRepository(ShortenedUrlContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Get a long url from a short url path
        public async Task<string> GetLongUrl(string path)
        {
            // Get the entire ShortenedUrl object from the database that matches the short path
            var shortenedUrl = await _context.ShortenedUrls.Where(shortUrl => shortUrl.Path.Equals(path))
                .FirstOrDefaultAsync();

            // If the shortened url object is null return null
            if (shortenedUrl == null)
                return null;

            // Check if the link is expired
            var expiration = DateTime.Parse(shortenedUrl.Expiration);

            // If it is, return null
            if (DateTime.Compare(expiration, DateTime.UtcNow) != 1)
                return null;

            // Use the short url
            shortenedUrl.UseShortUrl();

            // Save the database changes
            await SaveChanges();

            // Return only the OriginalUrl
            return shortenedUrl.OriginalUrl;
        }

        // Get the entire SimpleShortUrl object based on the given path
        public async Task<SimpleShortUrl> GetShortenedUrl(string path)
        {
            return await _context.ShortenedUrls.Where(shortUrl => shortUrl.Path.Equals(path))
                .Select(idvShortenedUrl => new SimpleShortUrl(idvShortenedUrl))
                .Where(shortUrl => DateTime.Compare(shortUrl.Expiration, DateTime.UtcNow) == 1)
                .FirstOrDefaultAsync();
        }

        // Get all SimpleShortUrl objects created by the given IpAddress
        public async Task<SimpleShortUrl[]> GetAllShortenedUrls(string ipAddress)
        {
            // Has the IpAddress before looking
            var hashedIpAddress = HashIpAddress(ipAddress);

            return await _context.ShortenedUrls
                .Where(shortUrl => shortUrl.IpAddress.Equals(hashedIpAddress))
                .Select(idvShortenedUrl => new SimpleShortUrl(idvShortenedUrl))
                .ToArrayAsync();
        }

        // Create a new shortened URL from model
        public async Task<SimpleShortUrl> CreateShortedUrl(CreateShortenedUrlModel model)
        {
            // Check if the path is taken
            if (await IsPathTaken(model.Path))
                return null;

            // Check if the path is valid
            if (await FilterService.FilterService.IsValid(model.Path))
                return null;

            // Has the IpAddress
            var hashedIpAddress = HashIpAddress(model.IpAddress);

            // Create the new object
            var shortenedUrl = new ShortenedUrl(hashedIpAddress, model.OriginalUrl, model.Path, model.Expiration);

            // Add the object to the database
            await _context.ShortenedUrls.AddAsync(shortenedUrl);

            // Save the changes to the database
            return await SaveChanges()
                ? new SimpleShortUrl(shortenedUrl)
                : null;
        }

        // Update an existing url
        public async Task<SimpleShortUrl> UpdateShortedUrl(UpdateShortenedUrlModel model)
        {
            // Hash the ip address
            var hashedIpAddress = HashIpAddress(model.IpAddress);

            // Check for an existing url created by the hashed IpAddress
            var existingUrl = await _context.ShortenedUrls
                .Where(shortUrl => shortUrl.IpAddress == hashedIpAddress)
                .FirstOrDefaultAsync();

            // If no url returned
            if (existingUrl == null)
                return null;

            // Check if the updated path is taken
            if (await IsPathTaken(model.Path))
                return null;

            // Check if the updated path is value
            if (await FilterService.FilterService.IsValid(model.Path))
                return null;

            // Update the existing Url
            existingUrl.Path = model.Path;
            existingUrl.OriginalUrl = model.OriginalUrl;
            existingUrl.Expiration = model.Expiration;
            
            // Update the url
            _context.ShortenedUrls.Update(existingUrl);

            // save the changes into the database
            return await SaveChanges()
                ? new SimpleShortUrl(existingUrl)
                : null;
        }

        // Delete a url from the database
        public async Task<RequestResponse> DeleteShortedUrl(DeleteShortenedUrlModel model)
        {
            // Hash the ip Address
            var hashedIpAddress = HashIpAddress(model.IpAddress);

            // Get the existing URl
            var existingUrl = await _context.ShortenedUrls
                .Where(shortUrl => shortUrl.IpAddress == hashedIpAddress)
                .FirstOrDefaultAsync();

            // Remove the url
            _context.ShortenedUrls.Remove(existingUrl);

            // Save the changes
            return await SaveChanges()
                ? new RequestResponse("Success", "Url has been deleted")
                : new RequestResponse("Failed", "Error while deleting url");
        }

        // Hash the Ip Address
        private string HashIpAddress(string ipAddress)
        {
            // Number of iterations for the hashing alg
            const int iterations = 5000;

            // Get the bytes from the given IpAddress and the application pepper
            var givenIpAddress = Encoding.UTF8.GetBytes($"{ipAddress}.{_configuration["Hashing:Pepper"]}");

            using var sha512 = SHA512.Create();

            // Hash it iterations times
            for (var i = 0; i < iterations; i++)
                givenIpAddress = sha512.ComputeHash(givenIpAddress);

            var stringBuilder = new StringBuilder();

            // For every byte get the char
            foreach (var idvByte in givenIpAddress)
                stringBuilder.Append(idvByte.ToString("x2"));

            // Return the hashed string
            return stringBuilder.ToString();
        }

        // Check if the path is taken
        private async Task<bool> IsPathTaken(string path)
            => await _context.ShortenedUrls.AnyAsync(shortUrl => shortUrl.Path.Equals(path));

        // Save the changes to the database
        private async Task<bool> SaveChanges()
            => await _context.SaveChangesAsync() > 0;
    }
}