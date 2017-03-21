using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightPhysics : MonoBehaviour
{
    // Plane specific variables
    [Tooltip("Maximum plane thurst. Values in range 0 .. +")]
    public float MaxThrust;
    //[Tooltip("Not actually used, see 'Rigidbody2D'->'linear drag' instead")]
    //public float Drag;
    [Tooltip("Affects strength of lift coefficient. No known range for values.")]
    public float Lift;
    [Tooltip("Amount of thrust. Values in range 0.0 .. 1.0"), Range(0.0f, 1.0f)]
    public float throttle;
    [Tooltip("Amount of force to raise or lower the nose. Values in range -1 .. 1"), Range(-1.0f, 1.0f)]
    public float pitchTorque;

    // Hidden variables, not all used
    private float liftCoefficient;
    private float AoA;
    private float tas;
    private float altitude;
    private float liftForce;

    // GameObject stuff
    private Rigidbody2D rb;

    //[Tooltip("Amount of thrust. Values in range 0.0 .. 1.0"), Range(0.0f, 1.0f)]
    public float Throttle
    {
        get { return throttle; }
        set { throttle = value; }
    }

    public float PitchTorque
    {
        get { return pitchTorque; }
        set { PitchTorque = pitchTorque; }
    }

    // Use this for initialization
    void Start()
    {
        Throttle = 0;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float pitch = Input.GetAxis("Horizontal");
        float throttle = Input.GetAxis("Vertical");

        if (pitch != 0.0f) { pitchTorque = pitch; }

        rb.AddTorque(pitchTorque, ForceMode2D.Force);
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
    }
}
