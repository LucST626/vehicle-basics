using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : ScriptableObject
{
    [Header("Movement")]
    public float playerSpeed;
    public float friction;
    public float acceleration;
    public float maxSpeed;
    public float brakeStrength;
    public float brakeFriction;
}

