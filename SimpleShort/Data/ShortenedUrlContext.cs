/*
 * ShortenedUrlContext:
 * Responsible for connecting to the database. We are using EntityFrameworkCore to connect and manage the database automatically.
 *
 * ShortenedUrls: ShortenedUrl = Database table holding all shortened url information.
 * Logs: LogEntity = Database table holding all log information
 */

using Microsoft.EntityFrameworkCore;
using SimpleShort.Entities;
using SimpleShort.Entities.Logging;

namespace SimpleShort.Data
{
    public class ShortenedUrlContext : DbContext
    {
        public ShortenedUrlContext(DbContextOptions<ShortenedUrlContext> options)
            : base(options) { }

        public DbSet<ShortenedUrl> ShortenedUrls { get; set; }
        public DbSet<LogEntity> Logs { get; set; }
    }
}