using UnityEngine;

public class DotMovement : MonoBehaviour
{
    [Header("Special Variables")]
    public float rotationSpeed;
    public float projectileSpeed;
    public Rigidbody2D rb;
    public bool hasReachMouse;
    public Transform returnPoint;
    public bool outOfEnergy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - (Vector2)transform.position;
        direction.Normalize();

        if (Vector2.Distance(transform.position, mousePosition) > 1) // Set normal chase behaviour
        {
            hasReachMouse = false;
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            transform.Rotate(0, 0, -rotateAmount * rotationSpeed * Time.deltaTime);

            transform.Translate(Vector2.up * projectileSpeed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            transform.Translate(Vector2.up * projectileSpeed * Time.deltaTime);
        }
    }
}
