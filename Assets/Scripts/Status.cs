using Obvious.Soap;
using System.Collections;
using UnityEngine;

public class Status : MonoBehaviour
{
    [Header("Private Variables")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Stun Status")]
    [SerializeField] private BoolVariable _stunStatus;
    [SerializeField] private float stunDuration;
    [SerializeField] private float knockbackForce;

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }

    IEnumerator ApplyStunAndKnockback(Vector2 pushDirection)
    {
        _stunStatus.Value = true;

        // Cancel movement and apply knockback (reset velocity and apply impulse)
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(pushDirection * knockbackForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(stunDuration);
        rb.linearVelocity = Vector2.zero;
        _stunStatus.Value = false;
    }
}
