namespace WeatherApp.Models;

public class GeolocationResult
{
    public bool Success { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Error { get; set; }
}

