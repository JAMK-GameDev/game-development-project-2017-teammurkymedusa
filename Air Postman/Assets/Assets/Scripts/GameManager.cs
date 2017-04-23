﻿using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class GameManager : MonoBehaviour
{
    public AudioSource[] Explosions;
    public PlaneDeathHandler PlaneDeathHandler;
    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    private Death Death;                       //Store a reference to our BoardManager which will set up the level.
    private int level = 3;                                  //Current level number, expressed in game as "Day 1".

	private GameObject player;
	private GameObject persistantObject;
	private ScoreManager scManager;

	private float scoreInterval = 10; // How often we score player (Distance)
	private float lastX; // Player's last X position

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        //Get a component reference to the attached BoardManager script
        Death = GetComponent<Death>();

		player = GameObject.FindGameObjectWithTag ("Player");
		persistantObject = GameObject.Find ("PersistantObject");
		//scManager = persistantObject.GetComponent<ScoreManager> ();
		//if (scManager.ScoreInterval != 0) {
			//scoreInterval = scManager.ScoreInterval;
		//}

		lastX = player.transform.position.x; // variable Initialization
    }
	void FixedUpdate()
	{
	    if (!player) return;
		if ((player.transform.position.x - lastX) > scoreInterval) {
			//Debug.Log("Player's X pos: " + player.transform.position.x);
			//scManager.addTravelScore (); // We are not passing any parameters here because needed parameters are defined within the ScoreManager
			lastX = player.transform.position.x;
		}
	}
    public void ActivateDeath(bool destroyPlane)
    {
		//scManager.SetHighScore ();
        this.Death.ActivateDeath();
        if (destroyPlane && PlaneDeathHandler.gameObject)
        {
            Destroy(PlaneDeathHandler.gameObject);
        }
    }

    public void ActivatePlaneBoom()
    {
        this.PlaneDeathHandler.BlownUpPlane();
        ExplosionSounds();
    }
    public void ExplosionSounds()
    {
        int startExplosion = Random.Range(0, Explosions.Length);
        Explosions[startExplosion].Play();
        StartCoroutine(SecondBoom(1 - startExplosion));
    }

    IEnumerator SecondBoom(int other)
    {
        yield return new WaitForSeconds(.35f);
        Explosions[other].Play();
    }
}
