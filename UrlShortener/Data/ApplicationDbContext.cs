using Microsoft.EntityFrameworkCore;
using UrlShortener.Models.Entities;

namespace UrlShortener.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<ShortUrl> ShortUrls { get; set; }
    public DbSet<UrlAccessLog> UrlAccessLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ShortUrl>()
            .HasIndex(s => s.ShortCode)
            .IsUnique();

        modelBuilder.Entity<ShortUrl>()
            .HasIndex(s => s.SecretCode)
            .IsUnique();

        modelBuilder.Entity<UrlAccessLog>()
            .HasOne(l => l.ShortUrl)
            .WithMany(s => s.AccessLogs)
            .HasForeignKey(l => l.ShortUrlId);
    }
}