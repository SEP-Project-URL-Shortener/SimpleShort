using System;

namespace SimpleShort.Entities
{
    public class SimpleShortUrl
    {
        public string OriginalUrl { get; set; }
        public string Path { get; set; }
        public DateTime Expiration { get; set; }

        public SimpleShortUrl(ShortenedUrl shortUrl)
        {
            OriginalUrl = shortUrl.OriginalUrl;
            Path = shortUrl.Path;
            Expiration = DateTime.Parse(shortUrl.Expiration);
        }

        public SimpleShortUrl(string originalUrl, string path, string expiration)
        {
            OriginalUrl = originalUrl;
            Path = path;
            Expiration = DateTime.Parse(expiration);
        }
    }
}
