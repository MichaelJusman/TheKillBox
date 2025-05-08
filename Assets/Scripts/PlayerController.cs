// --- PlayerController.cs ---
using UnityEngine;

public class PlayerController : MonoBehaviour
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
        Debug.Log("Player died");
        // Disable input, play animation, etc.
    }
}