using Microsoft.AspNetCore.Mvc;
using UrlShortener.Services.Interfaces;

namespace UrlShortener.Controllers;

[ApiController]
[Route("api/stats")]
public class StatsController(IStatsService statsService) : ControllerBase
{
    [HttpGet("{secretCode}")]
    public async Task<IActionResult> GetStats(string secretCode)
    {
        var stats = await statsService.GetStats(secretCode);
        if (stats == null)
            return NotFound("Stats not found");

        return Ok(stats);
    }
}