using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderFlash : MonoBehaviour
{
    [Tooltip("Time between flashes")]
    public float Frequency;

    private float timeAlive;

    // Time since last strike
    private float dtFlashStrike;

    private SpriteRenderer sr;

	// Use this for initialization
	void Start()
    {
        timeAlive = 0.25f;
        dtFlashStrike = 0.0f;

        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
	}
	
	// Update is called once per frame
	void Update()
    {
		if (dtFlashStrike > Frequency)
        {
            dtFlashStrike = 0.0f;
            sr.enabled = true;
        }
        else if (dtFlashStrike > timeAlive)
        {
            sr.enabled = false;
        }
        dtFlashStrike += Time.deltaTime;
	}

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player") && sr.enabled)
        {
            GameManager.instance.ActivateDeath();
        }
    }
}
