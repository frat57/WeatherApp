# WeatherApp

Kısa: Blazor ile hazırlanmış basit bir hava durumu uygulaması. Open‑Meteo API'den günlük ve saatlik veriler çekip gösterir.

## Hızlı başlangıç
1. .NET 8 SDK ve git kurulu olmalı.
2. Projeyi çalıştır:

```powershell
dotnet build WeatherApp/WeatherApp.csproj
dotnet run --project WeatherApp/WeatherApp.csproj
```

Tarayıcıda: https://localhost:5001/weather

## Kısa notlar
- API: Open‑Meteo (ücretsiz, anahtar gerektirmez) — https://open-meteo.com
- Koordinatlar InvariantCulture ile URL'e eklenir (lokal ondalık ayırıcı sorunlarına karşı)
- Tarih/saatler DateTimeOffset ile parse edilip yerel zamana çevriliyor (timezone sorunları için)

Ek olarak README'ye örnek ekran görüntüleri, katkı rehberi veya lisans eklemek isterseniz söyleyin.
