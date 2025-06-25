namespace UrlShortener.Models.Entities;

public class UrlAccessLog
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ShortUrlId { get; set; }
    public ShortUrl ShortUrl { get; set; } = null!;

    public DateTime AccessedAt { get; set; } = DateTime.UtcNow;
    public string IpAddress { get; set; } = null!;
}