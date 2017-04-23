using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedIndicator : MonoBehaviour
{
    public GameObject SpeedIndicatorNeedle;

    public float maxRotation;
    private Vector3 initialRotation;
    private GameObject Aircraft;
    private float speed;

	void Start()
    {
        Aircraft = Aircraft ?? GameObject.FindGameObjectWithTag("Player");
        SpeedIndicatorNeedle = SpeedIndicatorNeedle ?? GameObject.Find("SpeedIndicatorNeedle");
        initialRotation = SpeedIndicatorNeedle.transform.eulerAngles;
	}
	
	void FixedUpdate()
    {
        if (Aircraft)
        {
             speed = Aircraft.GetComponent<PlaneControls>().GetSpeed();
             Vector3 needleRotation = new Vector3(0.0f, 0.0f, initialRotation.z - speed * maxRotation);
             //Debug.Log(needleRotation + " ja " + initialRotation);
             SpeedIndicatorNeedle.transform.localEulerAngles = needleRotation;
        }

	}
}
