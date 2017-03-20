using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightControls : MonoBehaviour
{
    private Rigidbody2D rb;
    private FlightPhysics fp;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fp = GetComponent<FlightPhysics>();
	}

	// Update is called once per frame
	void Update()
    {
        float pitch = Input.GetAxis("Horizontal");
        float throttle = Input.GetAxis("Vertical");

        rb.AddTorque(pitch * -1, ForceMode2D.Force);
        fp.Throttle = (throttle + 1) / 2;
    }
}
