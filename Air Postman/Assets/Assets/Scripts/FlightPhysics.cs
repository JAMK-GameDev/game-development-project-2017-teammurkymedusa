﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightPhysics : MonoBehaviour
{
    // Plane specific variables
    [Tooltip("Maximum plane thurst. Values in range 0 .. +")]
    public float MaxThrust;
    [Tooltip("Not actually used, see 'Rigidbody2D'->'linear drag' instead")]
    public float Drag;
    [Tooltip("Affects strength of lift coefficient. No known range for values.")]
    public float Lift;

    [Tooltip("Amount of thrust. Values in range 0.0 .. 1.0"), Range(0.0f, 1.0f)]
    public float Throttle;

    // Hidden variables, not all used
    private float liftCoefficient;
    private float AoA;
    private float tas;
    private float altitude;
    private float liftForce;

    // GameObject stuff
    private Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        Throttle = 0;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Add thurst
        rb.AddRelativeForce(Vector2.up * MaxThrust * Throttle, ForceMode2D.Force);

        AoA = transform.rotation.eulerAngles.z - 200.0f;

        // Calculate lift coefficient and liftForce
        // No touchy, is magic, much math.
        // (x - 1.25) ** 3 - (x - 0.25) + 1.6
        // We also make sure the lift is lost if angle of attack is too much or less
        if (0.0f <= AoA && AoA <= 110.0f)
        {
            liftCoefficient = Mathf.Pow((AoA / 360 - 1.25f), 3.0f) - (AoA / 360 - 0.25f) + 1.6f;
        }
        else
        {
            liftCoefficient = 0.0f;
        }
        liftForce = Lift * liftCoefficient * rb.velocity.x;
        rb.AddRelativeForce(Vector2.left * liftForce, ForceMode2D.Force);
        print("Lift coefficient: " + liftCoefficient);
        print("Lift force: " + liftForce);
        print("Angle of Attack: " + (AoA - 70.0f));
    }
}