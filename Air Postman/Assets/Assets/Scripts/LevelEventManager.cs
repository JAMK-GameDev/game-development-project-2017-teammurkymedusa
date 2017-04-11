using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.events;
using UnityEngine;

public class LevelEventManager : MonoBehaviour {

    public GameObject BirdEventBase;
    public GameObject ProtoWind;
    public GameObject ProtoTurbulence;

    public int minDistance;

    public int ScreenLowestCoord;
    public int BottomScreenBuffer;

    public Camera camera;
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

        birdBounds = BirdEventBase.GetComponent<SpriteRenderer>();
        windBounds = ProtoWind.GetComponent<SpriteRenderer>();

        lastGenerationPoint = new Vector3(minDistance, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void PlaceEvents(int birdAmount, int windAmount)
    {
        for (int i = 0; i < birdAmount + windAmount; i++)
        {
            System.Random rnd = new System.Random(System.DateTime.Now.Millisecond);
            int eventType = rnd.Next(0,4);
            switch (eventType)
            {
                case 0:
                    generateBird(false, 3f, i);
                    break;
                case 1:
                    generateWind(Vector3.up, 2, new Vector2(100, 100), i);
                    break;
                case 2:
                    //TODO
                    break;
                case 3:
                    generateTurbulence(i);
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
        int ScreenHeight = (int) (camera.orthographicSize * 2f);
        lastGenerationPoint.x += minDistance + localRandom.Next(difficulty, 15);
        lastGenerationPoint.y = BirdEventBase.transform.position.y;
        float BirdHeight = localRandom.Next(ScreenLowestCoord+BottomScreenBuffer , ScreenLowestCoord + ScreenHeight);

        GameObject bird = Instantiate(BirdEventBase, lastGenerationPoint, Quaternion.identity, sky.transform);
        if (dynamite)
        {
            bird.name = "TntBird" + genNum;
        }
        else
            bird.name = "Bird" + genNum;
        //Set bird speed and dynamite
        bird.GetComponent<BirdEvent>().SetParameters(camera, 3f, BirdHeight, 1);

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
        //wind.GetComponent<WindEvent>().OnSpawn();
    }

    private void generateTurbulence(int genNum)
    {
        int ScreenHeight = (int)(camera.orthographicSize * 2f);
        lastGenerationPoint.x += minDistance + localRandom.Next(difficulty, 15);
        lastGenerationPoint.y = ProtoTurbulence.transform.position.y;
        float turbulence = localRandom.Next(ScreenLowestCoord + BottomScreenBuffer, ScreenLowestCoord + ScreenHeight);

        GameObject Turbulence = Instantiate(ProtoTurbulence, lastGenerationPoint, Quaternion.identity, sky.transform);
        Turbulence.name = "Turbulence" + genNum;
    }
}
