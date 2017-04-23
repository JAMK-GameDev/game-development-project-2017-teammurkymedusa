using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantObject : MonoBehaviour {

	private static PersistantObject instanceRef;
	// Use this for initialization
	void Awake () {
		if (instanceRef == null) {
			instanceRef = this;
			DontDestroyOnLoad (gameObject);
		} else {
			DestroyImmediate (gameObject);
		}

	}
	// Update is called once per frame
	void Update () {
		
	}
}
