using System.Text.Json;
using WeatherApp.Models;

namespace WeatherApp.Services;

public class WeatherService
{
    private readonly HttpClient _httpClient;
    private const string ForecastBaseUrl = "https://api.open-meteo.com/v1/forecast";
    private const string GeocodingBaseUrl = "https://geocoding-api.open-meteo.com/v1/search";

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherForecastResponse?> GetWeatherForecastAsync(double latitude, double longitude, string? timezone = null)
    {
        try
        {
            // Timezone'u belirle - eğer verilmemişse otomatik tespit et
            if (string.IsNullOrEmpty(timezone))
            {
                timezone = "auto";
            }

            // Use InvariantCulture when formatting doubles to avoid comma decimal separators on some locales (e.g. tr-TR)
            var latStr = latitude.ToString(System.Globalization.CultureInfo.InvariantCulture);
            var lonStr = longitude.ToString(System.Globalization.CultureInfo.InvariantCulture);
            var url = $"{ForecastBaseUrl}?latitude={latStr}&longitude={lonStr}&daily=temperature_2m_max,temperature_2m_min,weathercode,precipitation_sum,windspeed_10m_max&hourly=temperature_2m,relativehumidity_2m,weathercode,precipitation,windspeed_10m&timezone={timezone}&forecast_days=7";
            
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<WeatherForecastResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching weather data: {ex.Message}");
            return null;
        }
    }

    public async Task<List<LocationResult>> SearchLocationsAsync(string query, int limit = 10)
    {
        try
        {
            var url = $"{GeocodingBaseUrl}?name={Uri.EscapeDataString(query)}&count={limit}&language=tr&format=json";
            
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<GeocodingResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            
            return result?.Results ?? new List<LocationResult>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error searching locations: {ex.Message}");
            return new List<LocationResult>();
        }
    }

    public async Task<LocationResult?> GetLocationFromCoordinatesAsync(double latitude, double longitude)
    {
        try
        {
            // Open-Meteo reverse geocoding için farklı endpoint kullanılıyor
            // Önce search API'sini kullanarak en yakın konumu bulalım
            var latStr = latitude.ToString(System.Globalization.CultureInfo.InvariantCulture);
            var lonStr = longitude.ToString(System.Globalization.CultureInfo.InvariantCulture);
            var url = $"{GeocodingBaseUrl}?latitude={latStr}&longitude={lonStr}&count=1&language=tr&format=json";
            
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<GeocodingResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            
            // Eğer sonuç yoksa, alternatif olarak en yakın şehri bul
            if (result?.Results == null || result.Results.Count == 0)
            {
                // Alternatif: En yakın şehirleri ara
                var searchLat = latitude.ToString(System.Globalization.CultureInfo.InvariantCulture);
                var searchLon = longitude.ToString(System.Globalization.CultureInfo.InvariantCulture);
                var searchUrl = $"{GeocodingBaseUrl}?latitude={searchLat}&longitude={searchLon}&count=5&language=tr&format=json";
                var searchResponse = await _httpClient.GetAsync(searchUrl);
                if (searchResponse.IsSuccessStatusCode)
                {
                    var searchJson = await searchResponse.Content.ReadAsStringAsync();
                    var searchResult = JsonSerializer.Deserialize<GeocodingResponse>(searchJson, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return searchResult?.Results?.FirstOrDefault();
                }
            }
            
            return result?.Results?.FirstOrDefault();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting location from coordinates: {ex.Message}");
            return null;
        }
    }
}

