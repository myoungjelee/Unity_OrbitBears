/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySpot : MonoBehaviour
{
    public Transform gravityCenter;
    public float gravityStrength = 10f;
    public float landingRadius = 0.5f;

    public Rigidbody2D planetRigidbody;
    private bool isGravityActive = false; // 중력 활성화 여부

    void Start()
    {
        planetRigidbody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {

    }
    void FixedUpdate()
    {
        if (isGravityActive)
        {
            // 중력점과 행성사이의 벡터값을 계산
            Vector2 directionToCenter = (Vector2)gravityCenter.position - planetRigidbody.position;
            float distanceToCenter = directionToCenter.magnitude;

            // Calculate the gravity force magnitude (using the inverse square law)
            float gravityForceMagnitude = gravityStrength / (distanceToCenter * distanceToCenter);

            // Calculate the gravity vector
            Vector2 gravityForce = directionToCenter.normalized * gravityForceMagnitude;

            // Apply the gravity force to the ball
            planetRigidbody.AddForce(gravityForce);

            // Check if the ball is within the landing radius
            if (distanceToCenter <= landingRadius)
            {
                // Set the ball's velocity and position to zero to land it
                planetRigidbody.velocity = Vector2.zero;
                planetRigidbody.angularVelocity = 0.0f;
                planetRigidbody.position = gravityCenter.position;
            }
        }
    }
}*/