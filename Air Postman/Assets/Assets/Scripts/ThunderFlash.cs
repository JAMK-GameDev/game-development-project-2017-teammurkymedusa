using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderFlash : MonoBehaviour
{
    [Tooltip("Time between flashes")]
    public float Frequency;

    [Tooltip("Alert object to tell where thunder will hit")]
    public GameObject Alert;

    public GameObject Cloud;

    private float timeAlive;

    // Time since last strike
    private float dtFlashStrike;

    private SpriteRenderer sr;

    private float _cloudLength;

    private bool ThunderEdgeTrigger = false;

	// Use this for initialization
	void Start()
    {
        timeAlive = 0.25f;
        dtFlashStrike = 0.0f;

        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;

        _cloudLength = Cloud.GetComponent<SpriteRenderer>().bounds.size.x * 0.1f;

        Alert.SetActive(false);
	}
	
	// Update is called once per frame
	void Update()
    {
		if (dtFlashStrike > Frequency)
        {
            Alert.SetActive(false);
            dtFlashStrike = 0.0f;
            sr.enabled = true;
            if (!ThunderEdgeTrigger) ThunderEdgeTrigger = true;
        }
        else if (dtFlashStrike > timeAlive)
        {
            sr.enabled = false;
            if(ThunderEdgeTrigger)
            {
                //calculate new position
                Vector3 oldPos = transform.localPosition;
                float new_X = Random.Range(0 - _cloudLength / 2, _cloudLength / 2);
                oldPos.x = new_X;
                print(_cloudLength);
                transform.localPosition = oldPos;

                //put alert to new location
                Vector3 AlertPos = Alert.transform.localPosition;
                AlertPos.x = new_X;
                Alert.transform.localPosition = AlertPos;
                Alert.SetActive(true);
                ThunderEdgeTrigger = false;
            }
        }
        dtFlashStrike += Time.deltaTime;
	}

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player") && sr.enabled)
        {
            print("Flash collision!");
            GameManager.instance.ActivateDeath();
        }
    }
}
