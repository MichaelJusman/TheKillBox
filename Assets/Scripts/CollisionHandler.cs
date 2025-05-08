using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    [SerializeField] private MonoBehaviour damageableTarget;

    private IDamageable damageable;

    private void Awake()
    {
        damageable = damageableTarget as IDamageable;
    }

    public void ApplyDamage(float amount)
    {
        damageable?.TakeDamage(amount);
    }
}