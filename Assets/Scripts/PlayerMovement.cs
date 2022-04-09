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

    void FixedUpdate()
    {
        // Apply Force
        if (accelerationInput == 0)
        {
            vehicleRb.drag = Mathf.Lerp(
                vehicleRb.drag, dragFactor,
                Time.fixedDeltaTime * dragFactor
            );
        }
        else
        {
            vehicleRb.drag = 0;
        }
        Vector2 accelerationForce = transform.up * accelerationInput * accelerationFactor;
        vehicleRb.AddForce(accelerationForce, ForceMode2D.Force);

        // Apply Drift Factor
        Vector2 forwardVelocity = transform.up * Vector2.Dot(vehicleRb.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(vehicleRb.velocity, transform.right);
        vehicleRb.velocity = forwardVelocity + (rightVelocity * driftFactor);

        // Apply Steering
        float steeringMinSpeedFactor = Mathf.Clamp01(vehicleRb.velocity.magnitude / 2);
        rotationAngle -= steeringInput * turnFactor * steeringMinSpeedFactor;
        vehicleRb.MoveRotation(rotationAngle);
    }

    public void SetInput(Vector2 input)
    {
        steeringInput = input.x;
        accelerationInput = input.y;
    }
}
