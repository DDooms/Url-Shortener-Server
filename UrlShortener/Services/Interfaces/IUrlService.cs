using UrlShortener.Models.DTOs;

namespace UrlShortener.Services.Interfaces;

public interface IUrlService
{
    Task<ShortenUrlResponse> ShortenUrl(ShortenUrlRequest request);
    Task<string?> GetOriginalUrl(string shortCode);
}