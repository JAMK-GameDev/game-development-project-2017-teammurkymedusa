using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurbulenceEvent : MonoBehaviour {

    public Vector2 Direction;
    public float Magnitude;
    public float[] AreaOfEffect;

    private bool playerIn;
    private Collider2D player;
    //public float Pitch;
	// Use this for initialization
	void Start () {
        AreaOfEffect = new float[2];

        Direction = Vector3.down;
        Magnitude = 5;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (playerIn)
        {
            player.attachedRigidbody.AddForce(Direction * Magnitude);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            player = other;
            Debug.Log("Player entered Turbulence: " + gameObject.name);
            playerIn = true;
            InvokeRepeating("flipDirection", 1f, 2f);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Player is in Turbulence: " + gameObject.name);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            player = null;
            Debug.Log("Player exited Turbulence: " + gameObject.name);
            playerIn = false;
            CancelInvoke("flipDirection");
        }
    }

    //Coroutines
    private void flipDirection()
    {
        Direction = -1 * Direction;
    }
}
