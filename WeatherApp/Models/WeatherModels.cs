namespace WeatherApp.Models;

// UI Models for Weather Display
public class DailyWeather
{
    public DateTime Date { get; set; }
    public double MaxTemperature { get; set; }
    public double MinTemperature { get; set; }
    public int WeatherCode { get; set; }
    public double Precipitation { get; set; }
    public double MaxWindSpeed { get; set; }
    public string WeatherDescription => GetWeatherDescription(WeatherCode);
    public string WeatherIcon => GetWeatherIcon(WeatherCode);
    public List<HourlyWeather> HourlyData { get; set; } = new();

    private string GetWeatherDescription(int code)
    {
        return code switch
        {
            0 => "Açık",
            1 => "Çoğunlukla açık",
            2 => "Kısmen bulutlu",
            3 => "Kapalı",
            45 => "Sisli",
            48 => "Donlu sis",
            51 => "Hafif çisenti",
            53 => "Orta çisenti",
            55 => "Yoğun çisenti",
            56 => "Hafif donlu çisenti",
            57 => "Yoğun donlu çisenti",
            61 => "Hafif yağmur",
            63 => "Orta yağmur",
            65 => "Yoğun yağmur",
            66 => "Hafif donlu yağmur",
            67 => "Yoğun donlu yağmur",
            71 => "Hafif kar",
            73 => "Orta kar",
            75 => "Yoğun kar",
            77 => "Kar taneleri",
            80 => "Hafif sağanak",
            81 => "Orta sağanak",
            82 => "Yoğun sağanak",
            85 => "Hafif kar sağanağı",
            86 => "Yoğun kar sağanağı",
            95 => "Fırtına",
            96 => "Dolu ile fırtına",
            99 => "Şiddetli dolu ile fırtına",
            _ => "Bilinmeyen"
        };
    }

    private string GetWeatherIcon(int code)
    {
        return code switch
        {
            0 => "☀️",
            1 => "🌤️",
            2 => "⛅",
            3 => "☁️",
            45 or 48 => "🌫️",
            >= 51 and <= 67 => "🌧️",
            >= 71 and <= 77 => "❄️",
            >= 80 and <= 86 => "🌦️",
            >= 95 and <= 99 => "⛈️",
            _ => "🌤️"
        };
    }
}

public class HourlyWeather
{
    public DateTime Time { get; set; }
    public double Temperature { get; set; }
    public double Humidity { get; set; }
    public int WeatherCode { get; set; }
    public double Precipitation { get; set; }
    public double WindSpeed { get; set; }
    public string WeatherDescription => GetWeatherDescription(WeatherCode);
    public string WeatherIcon => GetWeatherIcon(WeatherCode);

    private string GetWeatherDescription(int code)
    {
        return code switch
        {
            0 => "Açık",
            1 => "Çoğunlukla açık",
            2 => "Kısmen bulutlu",
            3 => "Kapalı",
            45 => "Sisli",
            48 => "Donlu sis",
            51 => "Hafif çisenti",
            53 => "Orta çisenti",
            55 => "Yoğun çisenti",
            56 => "Hafif donlu çisenti",
            57 => "Yoğun donlu çisenti",
            61 => "Hafif yağmur",
            63 => "Orta yağmur",
            65 => "Yoğun yağmur",
            66 => "Hafif donlu yağmur",
            67 => "Yoğun donlu yağmur",
            71 => "Hafif kar",
            73 => "Orta kar",
            75 => "Yoğun kar",
            77 => "Kar taneleri",
            80 => "Hafif sağanak",
            81 => "Orta sağanak",
            82 => "Yoğun sağanak",
            85 => "Hafif kar sağanağı",
            86 => "Yoğun kar sağanağı",
            95 => "Fırtına",
            96 => "Dolu ile fırtına",
            99 => "Şiddetli dolu ile fırtına",
            _ => "Bilinmeyen"
        };
    }

    private string GetWeatherIcon(int code)
    {
        return code switch
        {
            0 => "☀️",
            1 => "🌤️",
            2 => "⛅",
            3 => "☁️",
            45 or 48 => "🌫️",
            >= 51 and <= 67 => "🌧️",
            >= 71 and <= 77 => "❄️",
            >= 80 and <= 86 => "🌦️",
            >= 95 and <= 99 => "⛈️",
            _ => "🌤️"
        };
    }
}

