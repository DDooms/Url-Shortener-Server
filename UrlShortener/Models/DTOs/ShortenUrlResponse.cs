namespace UrlShortener.Models.DTOs;

public class ShortenUrlResponse
{
    public string ShortUrl { get; set; } = null!;
    public string StatsUrl { get; set; } = null!;
}