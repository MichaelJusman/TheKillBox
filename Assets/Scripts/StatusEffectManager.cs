using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatusEffectManager : MonoBehaviour
{
    private Dictionary<StatusEffectType, StatusEffect> activeEffects = new();

    private void Update()
    {
        List<StatusEffectType> toRemove = new();

        foreach (var pair in activeEffects)
        {
            var effect = pair.Value;
            effect.timeRemaining -= Time.deltaTime;
            effect.onTick?.Invoke();

            if (effect.timeRemaining <= 0)
            {
                effect.onEnd?.Invoke();
                toRemove.Add(pair.Key);
            }
        }

        foreach (var type in toRemove)
        {
            activeEffects.Remove(type);
        }
    }

    public void ApplyEffect(StatusEffectType type, float duration, EffectApplicationMode mode)
    {
        if (!activeEffects.TryGetValue(type, out StatusEffect existing))
        {
            var newEffect = new StatusEffect(type, duration);
            RegisterEffect(newEffect);
            return;
        }

        switch (mode)
        {
            case EffectApplicationMode.Overwrite:
                existing.timeRemaining = duration;
                break;
            case EffectApplicationMode.StackDuration:
                existing.timeRemaining += duration;
                break;
            case EffectApplicationMode.StackDiminishing:
                existing.timeRemaining += duration * 0.5f;
                break;
            case EffectApplicationMode.IgnoreIfActive:
                break;
            case EffectApplicationMode.Overlap:
                StartCoroutine(HandleOverlapEffect(type, duration));
                break;
        }
    }

    private void RegisterEffect(StatusEffect effect)
    {
        activeEffects[effect.type] = effect;

        effect.onStart?.Invoke();

        switch (effect.type)
        {
            case StatusEffectType.Stun:
                effect.onStart = () => Debug.Log("Stunned");
                effect.onEnd = () => Debug.Log("Stun ended");
                break;

            case StatusEffectType.Burn:
                effect.onTick = () => Debug.Log("Burn tick");
                effect.onEnd = () => Debug.Log("Burn ended");
                break;
        }
    }

    private IEnumerator HandleOverlapEffect(StatusEffectType type, float duration)
    {
        Debug.Log($"Overlapping effect: {type}");
        yield return new WaitForSeconds(duration);
        Debug.Log($"Overlap duration ended: {type}");
    }

    public bool IsEffectActive(StatusEffectType type)
    {
        return activeEffects.ContainsKey(type);
    }
}