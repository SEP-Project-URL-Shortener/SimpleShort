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
