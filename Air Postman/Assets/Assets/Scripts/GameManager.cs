using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    private Death Death;                       //Store a reference to our BoardManager which will set up the level.
    private int level = 3;                                  //Current level number, expressed in game as "Day 1".

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

    }

    public void ActivateDeath()
    {
        this.Death.ActivateDeath();
    }
}
