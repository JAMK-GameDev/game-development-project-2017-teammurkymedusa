using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightControls : MonoBehaviour
{
    private Rigidbody2D rb;
    //private FlightPhysics fp;

    [Tooltip("Controls the torque that affects nose. Values in range -1.0 .. 1.0")]
    public float PitchControl = 0.0f;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //fp = GetComponent<FlightPhysics>();
	}

	// Update is called once per frame
	void Update()
    {
        //PitchControl = Input.GetAxis("Horizontal") * -1;
        float throttle = Input.GetAxis("Vertical");

        rb.AddTorque(PitchControl , ForceMode2D.Force);
        //fp.Throttle = (throttle + 1) / 2;
        PitchControl = 0.0f;
    }

    public void Pitch(float direction)
    {
        PitchControl = direction;
    }
}
