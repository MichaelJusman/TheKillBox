// --- EnemyController.cs ---
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Movement movement;
    private Health health;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        health = GetComponent<Health>();
    }

    private void Start()
    {
        health.OnDeath += HandleDeath;
    }

    private void HandleDeath()
    {
        Debug.Log("Enemy died");
        Destroy(gameObject);
    }
}