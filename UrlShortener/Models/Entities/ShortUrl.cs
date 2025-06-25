namespace UrlShortener.Models.Entities;

public class ShortUrl
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string OriginalUrl { get; set; } = null!;
    public string ShortCode { get; set; } = null!;
    public string SecretCode { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<UrlAccessLog> AccessLogs { get; set; } = new List<UrlAccessLog>();
}