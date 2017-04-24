using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderFlash : MonoBehaviour
{
    [Tooltip("Time between flashes")]
    public float Frequency;
    [Tooltip("Buffer Time between flashes")]
    public float ThunderMinBuffer = 1f;

    [Tooltip("Alert object to tell where thunder will hit")]
    public GameObject Alert;

    public GameObject Parent;

    public GameObject[] ThunderSounds;

    private float timeAlive;

    // Time since last strike
    private float dtFlashStrike;

    private SpriteRenderer sr;

    private float _cloudLength;

    private bool ThunderEdgeTrigger = false;
    private AudioSource ThunderSound;
    private bool _continueUpdate = true;

	// Use this for initialization
	void Start()
    {
        timeAlive = 0.25f;
        dtFlashStrike = 0.0f;
        Alert.SetActive(false);
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
	}

    public void Init(float length)
    {
        _cloudLength = length;
        int sound = Random.Range(0, ThunderSounds.Length);
        ThunderSound = Instantiate(ThunderSounds[sound]).GetComponent<AudioSource>();
    }
	// Update is called once per frame
	void Update()
	{
	    if (!_continueUpdate) return;
		if (dtFlashStrike > Frequency)
        {
            Alert.SetActive(false);
            dtFlashStrike = 0.0f;
            sr.enabled = true;
            ThunderSound.Play();
            if (!ThunderEdgeTrigger) ThunderEdgeTrigger = true;
        }
        else if (dtFlashStrike > timeAlive && sr.enabled)
        {
            sr.enabled = false;
            StartCoroutine(Wait());
            if(ThunderEdgeTrigger)
            {
                //if (dtFlashStrike < ThunderMinBuffer) return;
                //dtFlashStrike = 0.0f;
                //calculate new position
                Vector3 oldPos = transform.localPosition;
                float new_X = Random.Range(0 - _cloudLength / 2, _cloudLength / 2);
                oldPos.x = new_X;
                //print(_cloudLength);
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
    IEnumerator Wait()
    {
        _continueUpdate = false;
        yield return new WaitForSeconds(ThunderMinBuffer);
        _continueUpdate = true;
    }
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player") && sr.enabled)
        {
            //print("Flash collision!");
            GameManager.instance.ActivatePlaneBoom();
        }
    }
}
