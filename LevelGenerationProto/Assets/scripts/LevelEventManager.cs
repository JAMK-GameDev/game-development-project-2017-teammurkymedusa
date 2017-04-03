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
	void Start () {
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
        int eventAmount = birdAmount + windAmount;
        System.Random rnd = new System.Random(1);
        for (int i = 0; i < eventAmount; i++)
        {
            int eventType = rnd.Next(0, 1);

            switch (eventType)
            {
                case 0:
                    generateBird(false, 3f);
                    break;

                case 1:
                    generateWind(10f);
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="dynamite"></param>
    /// <param name="speed"></param>
    private void generateBird(bool dynamite, float speed)
    {
        lastGenerationPoint.x += minDistance + localRandom.Next(difficulty, 15);
        GameObject bird = Instantiate(ProtoBird, lastGenerationPoint, Quaternion.identity, sky.transform);
        //Set bird speed and dynamite

    }
    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="strength"></param>
    private void generateWind(float strength)
    {
        lastGenerationPoint.x += minDistance + localRandom.Next(difficulty, 15);
        GameObject wind = Instantiate(ProtoWind, lastGenerationPoint, Quaternion.identity, sky.transform);
        //Set wind strength
    }
}
