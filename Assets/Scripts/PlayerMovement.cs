using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float accelerationFactor = 10.0f;
    public float turnFactor = 3.5f;
    public float driftFactor = 0.95f;
    public Rigidbody2D vehicleRb;

    float accelerationInput = 0.0f;
    float steeringInput = 0.0f;
    float rotationAngle = 0.0f;
    float dragFactor = 3.0f;
    Vector2 lastInput = Vector2.zero;
    bool isBeingDamaged = false;
    float damageDuration = 0.0f;
    DynamicObstacleDamageType damageType;

    private void Update()
    {
        if (isBeingDamaged)
        {

            switch (damageType)
            {
                case DynamicObstacleDamageType.LowTraction:
                    break;
                case DynamicObstacleDamageType.Slow:
                    break;
                case DynamicObstacleDamageType.SpinNoSteering:
                    transform.Rotate(0.0f, 0.0f, 360.0f * Time.deltaTime);

                    break;
            }

            if (damageDuration > 0)
            {
                damageDuration -= Time.deltaTime;
            }
            else
            {
                damageDuration = 0.0f;
                isBeingDamaged = false;
            }
        }
    }

    void FixedUpdate()
    {

        float dragDamageFactor = 1.0f;
        float accelerationDamageFactor = 1.0f;
        float driftDamageFactor = 1.0f;
        bool applySeering = true;


        if (isBeingDamaged)
        {
            switch (damageType)
            {
                case DynamicObstacleDamageType.LowTraction:
                    dragDamageFactor = 0.0f;
                    driftDamageFactor = 4.0f;

                    break;
                case DynamicObstacleDamageType.Slow:
                    accelerationDamageFactor = 0.5f;
                    break;
                case DynamicObstacleDamageType.SpinNoSteering:
                    dragDamageFactor = 0.0f;
                    applySeering = false;
                    break;
            }
        }

        // Apply Drag
        vehicleRb.drag = CalculateDrag() * dragDamageFactor;

        // Apply Acceleration
        vehicleRb.AddForce(CalculateAccelerationForce() * accelerationDamageFactor, ForceMode2D.Force);

        // Apply Drift
        vehicleRb.velocity = CalculateDrift() * driftDamageFactor;

        // Apply Steering
        if (applySeering)
        {
            vehicleRb.MoveRotation(CalculateSteering());
        }
    }

    private float CalculateDrag()
    {
        if (accelerationInput == 0)
        {
            return Mathf.Lerp(
                vehicleRb.drag, dragFactor,
                Time.fixedDeltaTime * dragFactor
            );
        }
        else
        {
            return 0;
        }
    }
    private Vector2 CalculateAccelerationForce()
    {
        return transform.up * accelerationInput * accelerationFactor;
    }

    private Vector2 CalculateDrift()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(vehicleRb.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(vehicleRb.velocity, transform.right);
        return forwardVelocity + (rightVelocity * driftFactor);
    }

    private float CalculateSteering()
    {
        float steeringMinSpeedFactor = Mathf.Clamp01(vehicleRb.velocity.magnitude / 2);
        rotationAngle -= steeringInput * turnFactor * steeringMinSpeedFactor;
        return rotationAngle;
    }

    public void SetInput(Vector2 input)
    {
        // If the vehicle is being damaged, stop updating the input
        if (isBeingDamaged)
        {
            steeringInput = lastInput.x;
            accelerationInput = lastInput.y;
        }
        else
        {
            lastInput = input;
            steeringInput = input.x;
            accelerationInput = input.y;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DynamicObstacle"))
        {
            IDynamicObstacle obstacle = (IDynamicObstacle)collision.gameObject.GetComponent(typeof(IDynamicObstacle));
            InflictDynamicObstacleDamage(obstacle.DamageType, obstacle.DamageDuration);
        }
    }

    private void InflictDynamicObstacleDamage(DynamicObstacleDamageType type, float duration)
    {
        isBeingDamaged = true;
        damageType = type;
        damageDuration = duration;
    }
}
