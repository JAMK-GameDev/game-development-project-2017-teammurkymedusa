using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEvent : MonoBehaviour {

    //Attributes
    public float Magnitude;
    public Vector3 ForceVector; //Note Vector3.up is Vector3(0,1,0)
    public Vector2 AoE; //Are of effect, x,y

    //Components
    private BoxCollider2D myCol;
	// Use this for initialization
	void Start () {
        myCol = gameObject.GetComponent<BoxCollider2D>();
        myCol.size = AoE;
        //AoE[0] = 10f; // x
        //AoE[1] = 20f; // y, 20 is good value - it covers the whole play area
    }
	
    void OnTriggerStay(Collider player)
    {
        if (player.attachedRigidbody)
        {
            player.attachedRigidbody.AddForce(ForceVector * Magnitude);
        }
    }

    //This is used for destroying wind
    void OnTriggerExit(Collider player)
    {
        Destroy(gameObject);
        //gameObject.SetActive(false);
    }
}
