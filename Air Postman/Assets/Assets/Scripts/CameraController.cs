﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }
}