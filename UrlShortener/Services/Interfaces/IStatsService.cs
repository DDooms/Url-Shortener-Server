using UrlShortener.Models.DTOs;

namespace UrlShortener.Services.Interfaces;

public interface IStatsService
{
    Task LogVisit(string shortCode, string ipAddress);
    Task<StatsResponse?> GetStats(string secretCode);
}