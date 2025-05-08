using UnityEngine;
using Obvious.Soap;
using System;

public enum EaseType { Linear, SmoothStep, Quadratic, CustomCurve }

public class Movement : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private Vector3Variable _mousePosition;

    [Header("Status")]
    [SerializeField] private BoolVariable _stunStatus;


    [Header("Speed Stats")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float peakDistance;
    [SerializeField] private float stopDistance = 0.1f;

    [Header("Turning Stats")]
    [SerializeField] private float turnSpeed;
    private Vector3 faceDirection = Vector3.right;

    [Header("Testing")]
    public EaseType easeType;
    [SerializeField] private AnimationCurve speedCustomCurve;

    [Header("Testing Read Only")]
    [SerializeField] private float speedLinear;
    [SerializeField] private float speedSmoothStep;
    [SerializeField] private float speedQuadratic;
    [SerializeField] private float speedCustom;

    private void Update()
    {
        if (_stunStatus.Value) return;

        Vector3 moveTarget = _mousePosition.Value;
        Vector3 direction = (moveTarget - transform.position);
        float distance = direction.magnitude;

        //Stop movement when reaching the mouse
        if (distance < stopDistance) return;

        Vector3 targetDirection = direction.normalized;

        //Calculate rotation with turn rate constraint
        faceDirection = Vector3.RotateTowards(faceDirection, targetDirection, Mathf.Deg2Rad * turnSpeed * Time.deltaTime, float.MaxValue);
        faceDirection.z = 0;

        //Rotate the player object
        if (faceDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(faceDirection.y, faceDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        //Dynamic speed change steps
        float steps = Mathf.Clamp01((distance - stopDistance) / (peakDistance - stopDistance));

        //Testing different speed easing values
        speedLinear = Mathf.Lerp(0, maxSpeed, steps);
        speedSmoothStep = Mathf.SmoothStep(0, maxSpeed, steps);
        speedQuadratic = maxSpeed * steps * steps;
        speedCustom = maxSpeed * speedCustomCurve.Evaluate(steps);

        switch(easeType)
        {
            case EaseType.Linear:
                transform.position += faceDirection * speedLinear * Time.deltaTime;
                break;

            case EaseType.SmoothStep:
                transform.position += faceDirection * speedSmoothStep * Time.deltaTime;
                break;

            case EaseType.Quadratic:
                transform.position += faceDirection * speedQuadratic * Time.deltaTime;
                break;

            case EaseType.CustomCurve:
                transform.position += faceDirection * speedCustom * Time.deltaTime;
                break;
        }
    }

    private void OnDrawGizmos()
    {
        if(_mousePosition == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, peakDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stopDistance);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, _mousePosition.Value);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + faceDirection * 2f);
    }



}
