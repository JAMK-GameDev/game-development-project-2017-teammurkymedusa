using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderEvent : MonoBehaviour
{
    public GameObject Cloud;
    public float DebugLength;
    private BoxCollider2D collisionArea;
    private float Length;

	void Start()
    {
        //collisionArea = gameObject.GetComponent<BoxCollider2D>();
        //collisionArea.size = new Vector2(100.0f, Camera.main.orthographicSize * 2.0f);
	}

    public void Init()
    {
        GetComponentInChildren<ThunderFlash>().Init(Length);
    }
    public void SetLength(float length)
    {
        DebugLength = length;
        Length = length;
        collisionArea = gameObject.GetComponent<BoxCollider2D>();
        collisionArea.size = new Vector2(length, Camera.main.orthographicSize * 2.0f);
        Vector3 CloudPosition = new Vector3(0 - length / 2, Cloud.transform.localPosition.y, 0);
        float i = 0;
        Vector3 CloudSize = Cloud.GetComponent<SpriteRenderer>().bounds.size;
        while (i < length)
        {
            float flipper = 1;
            if(Random.Range(0, 2) == 1)
            {
                flipper = -1;
            }
            GameObject _Cloud = Instantiate(Cloud, CloudPosition, Quaternion.identity);
            _Cloud.transform.SetParent(gameObject.transform, false);
            i += CloudPosition.x + ( CloudSize.x / 2 );
            CloudPosition.x += Random.Range(CloudSize.x * 0.25f, CloudSize.x / 1.5f);
            CloudPosition.y += Random.Range(0, CloudSize.y  * 0.25f) * flipper;

        }
    }

    public float GetLength()
    {
        return Length;
    }
}
