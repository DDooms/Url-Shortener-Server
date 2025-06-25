namespace UrlShortener.Models.DTOs;

public class StatsResponse
{
    public List<VisitPerDay> UniqueVisitsPerDay { get; set; } = [];
    public List<IpStats> TopIps { get; set; } = [];
}

public class VisitPerDay
{
    public DateTime Date { get; set; }
    public int Count { get; set; }
}

public class IpStats
{
    public string Ip { get; set; } = null!;
    public int Count { get; set; }
}