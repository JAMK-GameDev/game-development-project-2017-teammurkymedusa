using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedIndicator : MonoBehaviour
{
    public GameObject SpeedIndicatorNeedle;
    private GameObject aircaft;
    private float initialRotation;
    private float speed;

	void Start()
    {
        aircaft = GameObject.FindGameObjectWithTag("Player");
        initialRotation = 165.0f;
	}
	
	void FixedUpdate()
    {
        speed = aircaft.GetComponent<PlaneControls>().GetSpeed() * aircaft.GetComponent<PlaneControls>().MaxSpeed * 10;
        Vector3 needleRotation = new Vector3(0.0f, 0.0f, initialRotation - speed);
        SpeedIndicatorNeedle.transform.localEulerAngles = needleRotation;
	}
}
