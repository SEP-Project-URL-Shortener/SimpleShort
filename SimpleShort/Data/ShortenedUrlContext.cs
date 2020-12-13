/*
 * ShortenedUrlContext:
 * Responsible for connecting to the database. We are using EntityFrameworkCore to connect and manage the database automatically.
 *
 * ShortenedUrls: ShortenedUrl = Database table holding all shortened url information.
 * Logs: LogEntity = Database table holding all log information
 */

// Included Libraries
using Microsoft.EntityFrameworkCore;
using SimpleShort.Entities;
using SimpleShort.Entities.Logging;

namespace SimpleShort.Data
{
    public class ShortenedUrlContext : DbContext
    {
        // Default constructor gets DbContextOptions via dependency injection
        public ShortenedUrlContext(DbContextOptions<ShortenedUrlContext> options)
            : base(options) { }

        // Database table named ShortenedUrls
        public DbSet<ShortenedUrl> ShortenedUrls { get; set; }

        // Database table named Logs
        public DbSet<LogEntity> Logs { get; set; }
    }
}