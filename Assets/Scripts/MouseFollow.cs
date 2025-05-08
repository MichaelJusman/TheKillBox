using System.Collections;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float minSpeed = 1f;
    public float speedMultiplier = 5f;
    public float stopDistance = 0.1f;
    public float knockbackForce = 5f;
    public float stunDuration = 0.5f;

    public bool isStunned = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isStunned) return;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector3 direction = mousePosition - transform.position;
        float distance = direction.magnitude;

        if (distance > stopDistance)
        {
            float speed = Mathf.Lerp(minSpeed, maxSpeed, distance / speedMultiplier);
            rb.linearVelocity = direction.normalized * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            

            StartCoroutine(Stun());

            Vector2 knockbackDirection = (transform.position - collision.transform.position);
            rb.linearVelocity = knockbackDirection * knockbackForce;
        }
    }

    IEnumerator Stun()
    {
        isStunned = true;
        rb.linearVelocity = Vector2.zero;  // Stop movement before knockback
        yield return new WaitForSeconds(stunDuration);
        isStunned = false;
    }
}
