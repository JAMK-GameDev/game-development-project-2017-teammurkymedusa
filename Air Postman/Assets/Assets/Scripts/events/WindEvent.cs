using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEvent : MonoBehaviour {

    //Attributes
    public Vector3 ForceVector; //Note Vector3.up is Vector3(0,1,0)
    public Vector2 AoE; //Are of effect, x,y
    public GameObject Particles_up;
    public GameObject Particles_down;

    //Components
    private BoxCollider2D myCol;
	// Use this for initialization
	void Start () {
        myCol = gameObject.GetComponent<BoxCollider2D>();
        myCol.size = AoE;
        //AoE[0] = 100f; // x
        //AoE[1] = 100f; // y
    }
	public void SetSize(Vector2 size)
    {
        myCol.size = size;
        AoE = size;
    }
    //Custom event, fired when object enters to players view
    public void OnSpawn()
    {
        myCol.size = AoE;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            GameObject activeParticleSystem = null;
            if (ForceVector.y <= 0)
            {
                Particles_up.SetActive(true);
                activeParticleSystem = Particles_up;
                double angle = Math.Atan(ForceVector.y / ForceVector.x) * (180/Math.PI);
                Vector3 currRot = activeParticleSystem.transform.localEulerAngles;
                Vector3 newRot = new Vector3(currRot.x, currRot.y, 90 + (float) angle * -1);
                activeParticleSystem.transform.localEulerAngles = newRot;
            } else
            {
                Particles_down.SetActive(true);
                activeParticleSystem = Particles_down;
                double angle = Math.Atan(ForceVector.y / ForceVector.x) * (180/Math.PI);
                Vector3 currRot = activeParticleSystem.transform.localEulerAngles;
                Vector3 newRot = new Vector3(currRot.x, currRot.y, 90 - (float) angle);
                activeParticleSystem.transform.localEulerAngles = newRot;
            }
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            //Debug.Log("Player hit " + gameObject.name);
            if (other.attachedRigidbody)
            {
                other.attachedRigidbody.AddForce(ForceVector);
            } 
        }
    }

    //This is used for destroying wind
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Destroy(gameObject); 
        }
        //gameObject.SetActive(false);
    }
}
