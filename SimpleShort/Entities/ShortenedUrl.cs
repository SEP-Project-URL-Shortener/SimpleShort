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
