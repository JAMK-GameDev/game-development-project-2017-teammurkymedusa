using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private Transform trans;
	// Use this for initialization
	void Start ()
	{
	    trans = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 25f;
        var y = Input.GetAxis("Vertical") * Time.deltaTime * 25f;

        trans.Translate(x,y,0);
    }
}
