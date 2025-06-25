using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using UrlShortener.Data;
using UrlShortener.Models.DTOs;
using UrlShortener.Services;
using UrlShortener.Services.Interfaces;

namespace UrlShortenerTests;

[TestClass]
public class UrlServiceTests
{
    private ApplicationDbContext _context = null!;
    private IUrlService _urlService = null!;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("UrlShortener_TestDb")
            .Options;

        _context = new ApplicationDbContext(options);

        var httpContext = new DefaultHttpContext
        {
            Request =
            {
                Scheme = "http",
                Host = new HostString("localhost")
            }
        };

        var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext);

        _urlService = new UrlService(_context, mockHttpContextAccessor.Object);
    }

    [TestMethod]
    public async Task ShortenUrl_ShouldReturnCorrectFormat()
    {
        var request = new ShortenUrlRequest { Url = "https://example.com" };

        var result = await _urlService.ShortenUrl(request);

        Assert.IsNotNull(result.ShortUrl);
        Assert.IsNotNull(result.StatsUrl);
        StringAssert.StartsWith(result.ShortUrl, "http://localhost/s/");
        StringAssert.StartsWith(result.StatsUrl, "http://localhost/stats/");
    }
}