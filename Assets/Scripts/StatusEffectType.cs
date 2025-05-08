using UnityEngine;

public enum StatusEffectType
{
    Stun, 
    Burn, 
    Freeze, 
    Poison, 
    Explosion, 
    Shock
}

public enum EffectApplicationMode
{
    Overwrite,
    StackDuration,
    StackDiminishing,
    IgnoreIfActive,
    Overlap
}

