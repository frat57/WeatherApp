namespace WeatherApp.Models;

// Open-Meteo Weather API Response Models
public class WeatherForecastResponse
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DailyForecast Daily { get; set; } = new();
    public HourlyForecast Hourly { get; set; } = new();
}

public class DailyForecast
{
    public List<string> Time { get; set; } = new();
    public List<double> Temperature_2m_Max { get; set; } = new();
    public List<double> Temperature_2m_Min { get; set; } = new();
    public List<int> Weathercode { get; set; } = new();
    public List<double> Precipitation_Sum { get; set; } = new();
    public List<double> Windspeed_10m_Max { get; set; } = new();
}

public class HourlyForecast
{
    public List<string> Time { get; set; } = new();
    public List<double> Temperature_2m { get; set; } = new();
    public List<double> Relativehumidity_2m { get; set; } = new();
    public List<int> Weathercode { get; set; } = new();
    public List<double> Precipitation { get; set; } = new();
    public List<double> Windspeed_10m { get; set; } = new();
}

