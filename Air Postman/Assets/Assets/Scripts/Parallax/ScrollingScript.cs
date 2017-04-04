// Modeled after http://pixelnest.io/tutorials/2d-game-unity/parallax-scrolling/
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScrollingScript : MonoBehaviour {

    public Vector2 Speed = new Vector2(2,2);
    public Vector2 Direction = new Vector2(-1,0);

    public bool isLinkedToCamera = false;

    public bool isLooping = false;

    private List<SpriteRenderer> backgroundPart;
	// Use this for initialization
	void Start () {

	    if (isLooping)
	    {
	        backgroundPart = new List<SpriteRenderer>();
	        for (int i = 0; i < transform.childCount; i++)
	        {
	            Transform child = transform.GetChild(i);
	            SpriteRenderer r = child.GetComponent<SpriteRenderer>();

	            //Add only the visible children
	            if (r != null)
	            {
	                backgroundPart.Add(r);
	            }
	        }

	        //Sort by position
	        // Note: Get the children from left to right.
	        // We would need to add a few conditions to handle
	        // all the possible scrolling directions.
	        backgroundPart = backgroundPart.OrderBy(t => t.transform.position.x).ToList();
	    }
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 movement = new Vector3(Speed.x *  Direction.x, Speed.y * Direction.y, 0);

	    //Movement
	    movement *= Time.deltaTime;
	    transform.Translate(movement);

	    //Move the camera
	    if (isLinkedToCamera)
	    {
	        Camera.main.transform.Translate(movement);
	    }

	    //Loop
	    if (isLooping)
	    {
	        // Get the first object.
	        // The list is ordered from left (x position) to right.
	        SpriteRenderer firstChild = backgroundPart.FirstOrDefault();

	        if (firstChild != null)
	        {
	            // Check if the child is already (partly) before the camera.
	            // We test the position first because the IsVisibleFrom
	            // method is a bit heavier to execute.

	            if (firstChild.transform.position.x < Camera.main.transform.position.x)
	            {
	                // If the child is already on the left of the camera,
	                // we test if it's completely outside and needs to be
	                // recycled.

	                if (firstChild.IsVisibleFrom(Camera.main) == false)
	                {
	                    // Get the last child position.
	                    SpriteRenderer lastChild = backgroundPart.LastOrDefault();
	                    Vector3 lastPosition = lastChild.transform.position;
	                    Vector3 lastSize = (lastChild.bounds.max - lastChild.bounds.min);

	                    // Set the position of the recyled one to be AFTER
	                    // the last child.
	                    // Note: Only work for horizontal scrolling currently.
	                    firstChild.transform.position = new Vector3(lastPosition.x + lastSize.x, firstChild.transform.position.y, firstChild.transform.position.z);

	                    // Set the recycled child to the last position
	                    // of the backgroundPart list.
	                    backgroundPart.Remove(firstChild);
	                    backgroundPart.Add(firstChild);
	                }
	            }
	        }
	    }
	}
}
