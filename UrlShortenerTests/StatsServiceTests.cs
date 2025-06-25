using Microsoft.EntityFrameworkCore;
using UrlShortener.Data;
using UrlShortener.Models.Entities;
using UrlShortener.Services;

namespace UrlShortenerTests;

[TestClass]
public class StatsServiceTests
{
    private ApplicationDbContext _context = null!;
    private StatsService _statsService = null!;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("StatsServiceTestDb")
            .Options;

        _context = new ApplicationDbContext(options);
        _statsService = new StatsService(_context);
    }

    [TestMethod]
    public async Task LogVisit_ShouldStoreAccessLog()
    {
        var shortUrl = new ShortUrl
        {
            Id = Guid.NewGuid(),
            OriginalUrl = "https://example.com",
            ShortCode = "abc123",
            SecretCode = "secretxyz"
        };

        _context.ShortUrls.Add(shortUrl);
        await _context.SaveChangesAsync();

        await _statsService.LogVisit("abc123", "192.168.0.10");

        var logs = _context.UrlAccessLogs.Where(x => x.ShortUrlId == shortUrl.Id).ToList();
        Assert.AreEqual(1, logs.Count);
        Assert.AreEqual("192.168.0.10", logs[0].IpAddress);
    }
}