# WeatherApp

Basit bir Blazor Weather uygulaması — Open‑Meteo API'si kullanılarak günlük ve saatlik hava durumu gösterir. Proje hem web hem de hareket halindeki (mobile) sürüm için temel bileşenleri içerir.

## Özellikler
- Konum arama ve reverse geocoding (Open‑Meteo geocoding)
- Tarayıcı konumunu kullanma (JS geolocation wrapper)
- 7 günlük günlük ve saatlik detaylar
- Zaman dilimi / saat parse hatalarını düzelten DateTimeOffset kullanan dönüşümler

## API
- Forecast: https://api.open-meteo.com/v1/forecast (ücretli değil, API anahtarı gerektirmez)
- Geocoding: https://geocoding-api.open-meteo.com/v1/search

> Not: Open‑Meteo ücretsiz bir servistir; kullanım limitleri/kurallar için resmi dokümantasyonunu kontrol edin.

## Hızlı başlatma (geliştirme)
1. Bu repository'yi klonlayın veya zaten içindeyseniz devam edin.
2. .NET SDK (8.0 veya üstü) kurulu olduğundan emin olun.

Geliştirme amaçlı web uygulamasını çalıştırmak için:

```powershell
dotnet build WeatherApp/WeatherApp.csproj
dotnet run --project WeatherApp/WeatherApp.csproj
```

Canlı değişiklikleri izlemek için:

```powershell
dotnet watch run --project WeatherApp/WeatherApp.csproj
```

Tarayıcıda `https://localhost:5001/weather` adresine gidin.

## Mobil / PWA seçenekleri
- Hızlı yol (en az efor): mevcut Blazor Web uygulamasını PWA olarak sunmak — tarayıcıda ana ekrana eklenebilir.
- Daha entegre native deneyim: .NET MAUI Blazor Hybrid — Razor bileşenlerini yeniden kullanarak native mobil uygulama oluşturabilirsiniz. Mevcut servisleri (WeatherService, GeolocationService ve modeller) paylaşılan bir class library olarak ayırmak iyi bir başlangıçtır.

## Geliştirme ipuçları
- API çağrılarını locale bağımsız hale getirmek için koordinatları InvariantCulture ile url'e ekledim.
- Zaman damgalarını parse ederken DateTimeOffset.TryParse kullanılıyor, sonrasında yerel zamana çevrilerek günlük/saatlik eşlemeler doğru yapılıyor.

# WeatherApp
Weather Application
