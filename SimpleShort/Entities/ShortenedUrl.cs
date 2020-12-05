/*
 * ShortenedUrl:
 * The object used for storing a short url in the database. I wish I could make some properties private to encapsulate it further from the user but with EF this is not possible.
 *
 * Id: string = Unique Id for ShortenedUrl.
 * IpAddress: string = Ip address of the user.
 * OriginalUrl: string = Original url to be shortened.
 * Path: string = The shortened url that redirects to the Original url.
 * Expiration: string = When the shortened url expires.
 * LastUsed: string = When was the shortened url last used.
 * Created: string = When was the shortened url created.
 * NumberOfUses: int = The number of uses the shortened url has.
 */

using System;
using Compute.Classes.Random;

namespace SimpleShort.Entities
{
    public class ShortenedUrl
    {
        public string Id { get; set; }
        public string IpAddress { get; set; }
        public string OriginalUrl { get; set; }
        public string Path { get; set; }
        public string Expiration { get; set; }
        public string LastUsed { get; set; }
        public string Created { get; set; }
        public int NumberOfUses { get; set; }

        public ShortenedUrl(string ipAddress, string originalUrl, string path = null, string expiration = null)
        {
            Id = Guid.NewGuid().ToString();
            IpAddress = ipAddress;
            OriginalUrl = originalUrl;
            Path = path ?? RandomPath();
            Expiration = expiration ?? DateTime.UtcNow.AddDays(30).ToString();
            LastUsed = null;
            Created = DateTime.UtcNow.ToString();
            NumberOfUses = 0;
        }

        public void UseShortUrl()
        {
            NumberOfUses++;
            LastUsed = DateTime.UtcNow.ToString();
        }

        private static string RandomPath()
        {
            var random = new Password(5,
                "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-._~:");
            return random.Generate();
        }
    }
}
