using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class CameraController : MonoBehaviour
{
    public Camera Cam;
    public float MinPos;
    public float MaxPos;
    public float StartPos;
    public PlaneControls Plane;

    private void Start()
    {
        Vector3 oldVec = Cam.transform.position;
        oldVec.x = StartPos;
        Cam.transform.position = oldVec;
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }

    private void LateUpdate()
    {
        Vector3 oldVec = Plane.gameObject.transform.position;
        float range;
        float fixedMin = MinPos;
        float fixedMax = MaxPos;
        if (MinPos < 0)
        {
            fixedMin *= -1;
        }
        else if (MaxPos < 0)
        {
            MaxPos *= -1;
        }
        range = MinPos + MaxPos;
        float y = 25f;
        float x = oldVec.x + (range * (1.35f - Plane.GetSpeed()) - fixedMin);
        //if (x < MinPos + MinPos * 0.4f) x -= fixedMin * 0.4f;
        //float x = oldVec.x;
        Vector3 NewVec = new Vector3(x,y, oldVec.z-10f);
        //Debug.Log("New X" + (range * (1f -Plane.throttle) - fixedMin));
        Cam.transform.position = NewVec;
    }
}
