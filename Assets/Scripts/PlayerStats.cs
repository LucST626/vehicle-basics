using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{
    [Header("Movement")]
    public float playerSpeed = 10.0f; 
    public float friction = 0.9f; 
    public float acceleration = 5.0f;
    public float accelerationAng = 9.0f;
    public float maxAngularSpeed = 9.0f;
    public float maxSpeed = 20.0f; 
    public float brakeStrength = 10.0f; 
    public float brakeFriction = 0.5f; 
    public float rotationSpeed = 5.0f; 

    [Header("Drift")]
    public float driftForce = 5.0f; // Adjust for drift force intensity
    public float minDriftSpeed = 5.0f; // Adjust for minimum speed to initiate drift
}
