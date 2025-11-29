from fastapi import FastAPI
from pydantic import BaseModel
from typing import List, Optional
import random

app = FastAPI(title="AI Ops Wizard - API (MVP)")


class RawEvent(BaseModel):
    id: Optional[str]
    timestamp: Optional[str]
    payload: dict


class ExplainEntry(BaseModel):
    feature: str
    importance: float


class AnalysisResult(BaseModel):
    id: Optional[str]
    score: float
    reasoning: str
    suggested_action: str
    explanation: List[ExplainEntry]


@app.get("/health")
async def health():
    return {"status": "ok"}


@app.post("/analyze", response_model=AnalysisResult)
async def analyze(event: RawEvent):
    """Minimal stub: returns a fake fraud score with natural language reasoning + suggested action.
    This is the shape your real ML/LLM pipeline should follow: score + human-readable reasoning + suggested action.
    """
    # NOTE: In a real system you would call your ML model and/or LLM here.
    fake_score = round(random.uniform(0, 1), 3)

    reasoning = (
        "Model observed unusual transaction velocity and mismatch between billing and shipping addresses. "
        "Several metadata fields correlated with previous fraud cases."
    )

    suggested_action = (
        "Hold the transaction and flag for manual review — contact the customer and require 2FA. "
        "If confirmed fraudulent, block the payment method and create a case in the CRM."
    )

    # small example explanation
    explain = [
        {"feature": "tx_velocity", "importance": 0.62},
        {"feature": "billing_shipping_mismatch", "importance": 0.27},
        {"feature": "device_anomaly_score", "importance": 0.11},
    ]

    return {
        "id": event.id,
        "score": fake_score,
        "reasoning": reasoning,
        "suggested_action": suggested_action,
        "explanation": explain,
    }
