# AI-Powered Operations Wizard — MVP (ops-wizard)

Bu klasör, MVP için hızlı bir başlangıç sağlayan minimal yapı ve docker-compose örneğini içerir. Amaç: Fraud Detection / Risk Compliance için "AI Core + Automation + Dashboard" mimarisi

## 1) Monorepo / proje yapısı (öneri)

- ops-wizard/
  - docker-compose.yml         # n8n, Postgres, backend(api) için
  - backend/                   # FastAPI (AI Core API) örneği
    - Dockerfile
    - requirements.txt
    - app/
      - main.py                # health + analyze (stub)

Bu yapı MVP için yeterli – gerçek prod'da AI core, model storage, XAI pipeline, auth, secrets ayrı servisler olmalı.

## 2) Veri akışı (örnek: Fraud Alert)

1. Ham veri (ör. ödeme event'i, e-posta, log) kaynak sistemlere (ör. payment gateway) ulaşır.
2. Bu veri bir webhook veya ETL enpoint ile n8n'e gelir (n8n Workflows giriş noktası).
3. n8n workflow veriyi temizler / zenginleştirir / normalleştirir (ör. geo IP, device fingerprint) ve `POST` ile AI Core (FastAPI) `/analyze` endpoint'ine gönderir.
4. AI Core:
   - hızlı scoring için lokal ML model veya kuralları çalıştırır (score)
   - karmaşık durumlarda LLM (OpenAI/Claude) çağırarak doğal dilde analiz ve öneri üretir
   - XAI bileşeni (feature importances, example-based justification) sonuçla beraber döner
5. API yanıtı (score + reasoning + suggested_action + explanation) n8n'e döner.
6. n8n bu çıktıya göre branch'ler: otomatik blok, beklet veya insan müdahalesi gerektiriyor ise dashboard'a ve Slack/CRM'e bildirim atar.
7. Dashboard (Next.js) n8n üzerinden veya API'den verileri çekip gösterir; kullanıcı manuel onay/ret yapıp n8n üzerinden follow-up aksiyon tetikler (ör. CRM case, müşteri araması).

## 3) "Wizard" mantığı — API sözleşmesi (MVP)

API, sadece bir sayısal skor dönmez; aynı zamanda insanın anlayacağı bir "reasoning" ve n8n'in kolayca tüketebileceği bir `suggested_action` döndürür.

Response (örnek):
```json
{
  "id": "evt-123",
  "score": 0.87,
  "reasoning": "Model observed high velocity + billing/shipping mismatch...",
  "suggested_action": "HOLD_TRANSACTION_AND_MANUAL_REVIEW",
  "explanation": [ { "feature": "tx_velocity", "importance": 0.62 }, ... ]
}
```

Bu sayede n8n workflow'ları yanıtı parse edip, action enum'una göre branch'leyebilir veya LLM önerisini direkt CRM/SLA eylem talimatına çevirip downstream sistemlere aktarabilir.

## 4) Nasıl çalıştırırsınız (lokal / MVP)

Kök dizinde (bu ops-wizard klasörü) aşağıyı çalıştırın:

```powershell
cd ops-wizard
docker compose up --build
```

- n8n: http://localhost:5678 (basic auth: admin / password)
- API: http://localhost:8000

Örnek test: curl ile analiz isteği

```bash
curl -X POST http://localhost:8000/analyze -H 'Content-Type: application/json' -d '{"id":"evt-1","payload": {"amount": 102.5}}'
```

---
Bu README MVP için bir başlangıç; isterseniz bir sonraki adım olarak:
- n8n workflow örnekleri (ops-wizard/n8n_workflows/),
- Auth / secrets yönetimi (env/.env, Vault),
- Basit Next.js (dashboard) örneği ekleyip webhook ile entegre edebilirim.
