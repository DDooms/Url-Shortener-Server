using Microsoft.EntityFrameworkCore;
using UrlShortener.Data;
using UrlShortener.Models.DTOs;
using UrlShortener.Models.Entities;
using UrlShortener.Services.Interfaces;

namespace UrlShortener.Services;

public class StatsService(ApplicationDbContext context) : IStatsService
{
    public async Task LogVisit(string shortCode, string ipAddress)
    {
        var shortUrl = await context.ShortUrls
            .FirstOrDefaultAsync(x => x.ShortCode == shortCode);

        if (shortUrl == null) return;

        var log = new UrlAccessLog
        {
            ShortUrlId = shortUrl.Id,
            IpAddress = ipAddress
        };

        context.UrlAccessLogs.Add(log);
        await context.SaveChangesAsync();
    }

    public async Task<StatsResponse?> GetStats(string secretCode)
    {
        var shortUrl = await context.ShortUrls
            .FirstOrDefaultAsync(x => x.SecretCode == secretCode);

        if (shortUrl == null) return null;

        var logs = await context.UrlAccessLogs
            .Where(x => x.ShortUrlId == shortUrl.Id)
            .ToListAsync();

        var uniqueVisits = logs
            .GroupBy(x => new { x.IpAddress, Date = x.AccessedAt.Date })
            .GroupBy(x => x.Key.Date)
            .Select(g => new VisitPerDay
            {
                Date = g.Key,
                Count = g.Count()
            })
            .OrderBy(x => x.Date)
            .ToList();

        var topIps = logs
            .GroupBy(x => x.IpAddress)
            .Select(g => new IpStats
            {
                Ip = g.Key,
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .Take(10)
            .ToList();

        return new StatsResponse
        {
            UniqueVisitsPerDay = uniqueVisits,
            TopIps = topIps
        };
    }
}