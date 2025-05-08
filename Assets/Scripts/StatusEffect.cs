using System;

public class StatusEffect
{
    public StatusEffectType type;
    public float duration;
    public float timeRemaining;

    public Action onStart;
    public Action onEnd;
    public Action onTick;

    public StatusEffect(StatusEffectType type, float duration)
    {
        this.type = type;
        this.duration = duration;
        this.timeRemaining = duration;
    }
}