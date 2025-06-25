using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models.DTOs;

public class ShortenUrlRequest
{
    [Required]
    [Url]
    [MaxLength(2048)]
    public string Url { get; set; } = null!;
}