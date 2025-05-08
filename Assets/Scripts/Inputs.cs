using UnityEngine;
using Obvious.Soap;

public class Inputs : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Camera mainCamera;

    [Header("Mouse Inputs")]
    [SerializeField] private Vector3Variable _mousePosition;

    private void Awake()
    {
        if(mainCamera == null) mainCamera = Camera.main;
    }

    private void Update()
    {
        Vector3 screenPosition = Input.mousePosition;
        screenPosition.z = -mainCamera.transform.position.z;

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0f;

        _mousePosition.Value = worldPosition;
    }
}
