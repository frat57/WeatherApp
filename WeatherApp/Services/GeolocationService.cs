using Microsoft.JSInterop;
using WeatherApp.Models;

namespace WeatherApp.Services;

public class GeolocationService
{
    private readonly IJSRuntime _jsRuntime;

    public GeolocationService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<(double Latitude, double Longitude)?> GetCurrentLocationAsync()
    {
        try
        {
            var result = await _jsRuntime.InvokeAsync<GeolocationResult>("getCurrentPosition");
            if (result != null && result.Success)
            {
                return (result.Latitude, result.Longitude);
            }
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting geolocation: {ex.Message}");
            return null;
        }
    }
}

