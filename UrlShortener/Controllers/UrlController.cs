using Microsoft.AspNetCore.Mvc;
using UrlShortener.Models.DTOs;
using UrlShortener.Services.Interfaces;

namespace UrlShortener.Controllers;

[ApiController]
[Route("api/url")]
public class UrlController(IUrlService urlService, IStatsService statsService, ILogger<UrlController> logger)
    : ControllerBase
{
    private readonly ILogger<UrlController> _logger = logger;

    [HttpPost("shorten")]
    public async Task<IActionResult> Shorten([FromBody] ShortenUrlRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await urlService.ShortenUrl(request);
        return Ok(response);
    }

    [HttpGet("/s/{shortCode}")]
    public async Task<IActionResult> RedirectToOriginal(string shortCode)
    {
        var originalUrl = await urlService.GetOriginalUrl(shortCode);
        if (originalUrl == null)
            return NotFound("Short URL not found");

        var ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault()
                 ?? HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        
        await statsService.LogVisit(shortCode, ip);

        return Redirect(originalUrl);
    }
}