using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyScript : MonoBehaviour
{
    public bool isWind;
    private bool hasSpawn;

    private Collider2D colliderComponent;

    private SpriteRenderer rendererComponent;

    private void Awake()
    {

        colliderComponent = GetComponent<Collider2D>();
        if (!isWind)
        {
            rendererComponent = GetComponent<SpriteRenderer>();
        }
    }

    // Use this for initialization
	void Start ()
	{

	    hasSpawn = false;
	    colliderComponent.enabled = false;
	    rendererComponent.enabled = false;
	    //Disable other scripts enemy object might have
	}
	
	// Update is called once per frame
	void Update () {
	    if (!hasSpawn)
	    {
	        if (rendererComponent.IsVisibleFrom(Camera.main))
	        {
	            Spawn();
	        }
	    }
	    else
	    {
	        //Is object out of camera? If so, we destroy it
	        if (!rendererComponent.IsVisibleFrom(Camera.main))
	        {
	            Debug.Log("Destroying " + gameObject.name);
	            Destroy(gameObject);
	        }
	    }
	}

    private void Spawn()
    {
        //Debug.Log(gameObject.name + " Spawned");
        colliderComponent.enabled = true;
        rendererComponent.enabled = true;

        //Enable other scripts and components
        if (isWind)
        {
            gameObject.GetComponent<WindEvent>().OnSpawn();
        }
    }
}
