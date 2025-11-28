namespace WeatherApp.Models;

// Open-Meteo Geocoding API Response Models
public class GeocodingResponse
{
    public List<LocationResult> Results { get; set; } = new();
}

public class LocationResult
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Country { get; set; } = string.Empty;
    public string Admin1 { get; set; } = string.Empty; // State/Province
    public string? Timezone { get; set; }
    
    public string DisplayName => $"{Name}, {Admin1}, {Country}";
}

