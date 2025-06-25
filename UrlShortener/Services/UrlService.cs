using Microsoft.EntityFrameworkCore;
using UrlShortener.Data;
using UrlShortener.Models.DTOs;
using UrlShortener.Models.Entities;
using UrlShortener.Services.Interfaces;

namespace UrlShortener.Services;

public class UrlService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    : IUrlService
{
    public async Task<ShortenUrlResponse> ShortenUrl(ShortenUrlRequest request)
    {
        var shortCode = GenerateRandomCode(6);
        var secretCode = GenerateRandomCode(24);

        var baseUrl = $"{httpContextAccessor.HttpContext!.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";

        var shortUrl = new ShortUrl
        {
            OriginalUrl = request.Url,
            ShortCode = shortCode,
            SecretCode = secretCode
        };

        context.ShortUrls.Add(shortUrl);
        await context.SaveChangesAsync();

        return new ShortenUrlResponse
        {
            ShortUrl = $"{baseUrl}/s/{shortCode}",
            StatsUrl = $"{baseUrl}/stats/{secretCode}"
        };
    }

    public async Task<string?> GetOriginalUrl(string shortCode)
    {
        var url = await context.ShortUrls
            .Where(x => x.ShortCode == shortCode)
            .Select(x => x.OriginalUrl)
            .FirstOrDefaultAsync();

        return url;
    }

    private static string GenerateRandomCode(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}