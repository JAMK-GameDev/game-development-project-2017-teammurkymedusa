using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedIndicator : MonoBehaviour
{
    public GameObject Aircraft;
    public GameObject SpeedIndicatorNeedle;
    private Vector3 initialRotation;
    private float speed;

	void Start()
    {
        Aircraft = Aircraft ?? GameObject.FindGameObjectWithTag("Player");
        SpeedIndicatorNeedle = SpeedIndicatorNeedle ?? GameObject.Find("SpeedIndicatorNeedle");
        initialRotation = transform.eulerAngles;
	}
	
	void FixedUpdate()
    {
        speed = Aircraft.GetComponent<PlaneControls>().GetSpeed();
        Vector3 needleRotation = new Vector3(0.0f, 0.0f, speed - initialRotation.z);
        SpeedIndicatorNeedle.transform.localEulerAngles = needleRotation;
	}
}
