using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEventManager : MonoBehaviour {

    public GameObject ProtoBird;
    public GameObject ProtoWind;

    public int minDistance;

    //Private variables
    private GameObject sky;
    private int difficulty;
    private System.Random pseudoRandom;
    private System.Random localRandom;
    SpriteRenderer birdBounds;
    SpriteRenderer windBounds;

    Vector3 lastGenerationPoint;

    float minEventDistance = 15;
	// Use this for initialization
	void Awake () {
        sky = gameObject.GetComponent<LevelGenerator>().Sky;
        difficulty = gameObject.GetComponent<LevelGenerator>().Difficulty;
        pseudoRandom = gameObject.GetComponent<LevelGenerator>().getPseudoRandom();
        localRandom = new System.Random(1);

        birdBounds = ProtoBird.GetComponent<SpriteRenderer>();
        windBounds = ProtoWind.GetComponent<SpriteRenderer>();

        lastGenerationPoint = new Vector2(minDistance, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void PlaceEvents(int birdAmount, int windAmount)
    {
        for (int i = 0; i < birdAmount + windAmount; i++)
        {
            System.Random rnd = new System.Random(System.DateTime.Now.Millisecond);
            int eventType = rnd.Next(0,1);
            switch (eventType)
            {
                case 0:
                    generateBird(false, 3f, i);
                    break;
                case 1:
                    generateWind(Vector3.up, 2, new Vector2(100, 100), i);
                    break;
                default:
                    break;
            }
        }
    }
    public void PlaceEventsOld(int birdAmount, int windAmount)
    {
        int eventAmount = birdAmount + windAmount;
        System.Random rnd = new System.Random(1);
        for (int i = 0; i < eventAmount; i++)
        {
            int eventType = rnd.Next(0, 1);
            Debug.Log("Randomized event type: " + eventType);
            switch (eventType)
            {
                case 0:
                    generateBird(false, 3f, i);
                    break;

                case 1:
                    generateWind(Vector3.up,10f,new Vector2(100,100), i);
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dynamite"></param>
    /// <param name="speed"></param>
    /// <param name="genNum"></param>
    private void generateBird(bool dynamite, float speed, int genNum)
    {
        lastGenerationPoint.x += minDistance + localRandom.Next(difficulty, 15);
        GameObject bird = Instantiate(ProtoBird, lastGenerationPoint, Quaternion.identity, sky.transform);
        if (dynamite)
        {
            bird.name = "TntBird" + genNum;
        }
        else
            bird.name = "Bird" + genNum;
        //Set bird speed and dynamite

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="force"></param>
    /// <param name="Magnitude"></param>
    /// <param name="area"></param>
    /// <param name="genNum"></param>
    private void generateWind(Vector3 force, float Magnitude, Vector2 area, int genNum)
    {
        lastGenerationPoint.x += minDistance + localRandom.Next(difficulty, 15);
        GameObject wind = Instantiate(ProtoWind, lastGenerationPoint, Quaternion.identity, sky.transform);
        //Set wind strength
        wind.GetComponent<WindEvent>().ForceVector = force;
        wind.GetComponent<WindEvent>().Magnitude = Magnitude;
        wind.GetComponent<WindEvent>().AoE = area;
        wind.name = "WindEvent" + genNum;
    }
}
