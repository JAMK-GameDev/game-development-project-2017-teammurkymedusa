using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderEvent : MonoBehaviour
{
    private BoxCollider2D collisionArea;

	void Start()
    {
        collisionArea = GetComponent<BoxCollider2D>();
        collisionArea.size = new Vector2(100.0f, Camera.main.orthographicSize * 2.0f);
	}
}
