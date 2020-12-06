/*
 * SimpleShortUrl:
 * The object used as an adapter to show a user information they can use.
 *
 * OriginalUrl: string = Original url to be shortened.
 * Path: string = The shortened url that redirects to the Original url.
 * Expiration: string = When the shortened url expires.
 */

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