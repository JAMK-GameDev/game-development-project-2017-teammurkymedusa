using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.events;
using UnityEngine;

public class LevelEventManager : MonoBehaviour {

    public GameObject BirdEventBase;
    public GameObject WhirlwindEventBase;
    public GameObject ProtoWind;
    public GameObject ProtoTurbulence;
    public GameObject ThunderEvent;

    public int minDistance;

    public int ScreenLowestCoord;
    public int BottomScreenBuffer;

    public Camera camera;
    //Private variables
    private GameObject sky;
    private int difficulty;
    private System.Random pseudoRandom;
    private System.Random localRandom;
    private int _screenHeight;
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
	void Start()
    {
         _screenHeight = (int)(camera.orthographicSize * 2f);
    }
	// Update is called once per frame
	void Update () {
		
	}
    public void PlaceEvents(int birdAmount, int windAmount)
    {

        for (int i = 0; i < birdAmount + windAmount; i++)
        {
            //System.Random rnd = new System.Random(System.DateTime.Now.Millisecond);
            int eventType = Random.Range(0,3);
            //Debug.Log("Randomized event type: " + eventType.ToString());
            switch (eventType)
            {
                case 0:
                   generateBird(false, 3f, i);
                    break;
                case 1:
                    generateWhirlwind(i);
                    break;
                case 2:
                    generateThunder(i);
                    break;
                default:
                    break;
            }
        }
    }
    public void PlaceEnviromentals(int windAmount, int turbulenceAmounts)
    {
        _screenHeight = (int)(camera.orthographicSize * 2f);
        lastGenerationPoint = new Vector3(minDistance, 0, 0f);
        for (int i = 0; i < turbulenceAmounts + windAmount; i++)
        {
            int eventType = Random.Range(0, 2);
            switch (eventType)
            {
                case 0:
                    generateWind(i);
                    break;
                case 1:
                    generateTurbulence(i);
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
            int eventType = rnd.Next(0, 3);
            //Debug.Log("Randomized event type: " + eventType.ToString());
            switch (eventType)
            {
                case 0:
                    generateBird(false, 3f, i);
                    break;

                case 1:
                    //generateWind(Vector3.up, 10f, new Vector2(100, 100), i);
                    break;
                case 2:
                    generateWhirlwind(i);
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
        bird.GetComponent<BirdEvent>().SetParameters(camera, 7.5f, BirdHeight, 1);

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="force"></param>
    /// <param name="Magnitude"></param>
    /// <param name="area"></param>
    /// <param name="genNum"></param>
    private void generateWind(int genNum)
    {
        float length = Random.Range(30f, 80f);
        lastGenerationPoint.x += minDistance + length + localRandom.Next(difficulty, 15);
        lastGenerationPoint.y = ProtoWind.transform.position.y;
        GameObject wind = Instantiate(ProtoWind, lastGenerationPoint, Quaternion.identity, sky.transform);

        //generate random direction (force vector). Magnitude will make it strong, so this will just have range of 0-1
        float flipper = 1;
        if(Random.Range(0, 2) == 1)
        {
            flipper = -1;
        }
        Vector2 forceVector = new Vector2(Random.value, Random.value * flipper);
        //now lets generate strength of wind
        forceVector *= Random.Range(5f, 10f);
        //now generate area

        Vector2 Area = new Vector2(length, ScreenLowestCoord + _screenHeight);
        //Set wind strength
        wind.GetComponent<WindEvent>().ForceVector = forceVector;
        wind.GetComponent<WindEvent>().AoE = Area;
        wind.name = "WindEvent" + genNum.ToString();
        //wind.GetComponent<WindEvent>().OnSpawn();
    }

    private void generateTurbulence(int genNum)
    {
        int ScreenHeight = (int)(camera.orthographicSize * 2f);
        lastGenerationPoint.x += minDistance + localRandom.Next(difficulty, 15);
        float Height = localRandom.Next(ScreenLowestCoord + BottomScreenBuffer, ScreenLowestCoord + ScreenHeight);
        lastGenerationPoint.y = Height;
        float Magnitude = Random.Range(2f, 6f);

        GameObject Turbulence = Instantiate(ProtoTurbulence, lastGenerationPoint, Quaternion.identity, sky.transform);
        Turbulence.name = "Turbulence" + genNum.ToString();
    }

    private void generateWhirlwind(int genNum)
    {
        lastGenerationPoint.x += minDistance + localRandom.Next(1,difficulty + 10);
        lastGenerationPoint.y = BirdEventBase.transform.position.y;
        GameObject whirlwind = Instantiate(WhirlwindEventBase, lastGenerationPoint, Quaternion.identity, sky.transform);
        int ScreenHeight = (int) (camera.orthographicSize * 2f);
        float WwHeight = localRandom.Next(ScreenLowestCoord+BottomScreenBuffer , ScreenLowestCoord + ScreenHeight);
        //calculate pull force
        //todo higher durability, more weight, less pull
        float pull = Random.Range(8f,15f);
        //calculate pull area

        //calculate speed
        //todo apply difficulty value
        float speed = Random.Range(3f,10f);
        whirlwind.GetComponent<WhirlwindEvent>().SetParameters(WwHeight, pull, 1.5f, speed);
    }

    private void generateThunder(int genNum)
    {
        //generate some width for thunder storm
        float length = Random.Range(50f, 100f);
        lastGenerationPoint.x += minDistance + localRandom.Next(1,difficulty + 10) + length;
        lastGenerationPoint.y = ThunderEvent.transform.position.y;
        GameObject Thunder = Instantiate(ThunderEvent, lastGenerationPoint, Quaternion.identity, sky.transform);

        Thunder.GetComponent<ThunderEvent>().SetLength(length);
        Thunder.GetComponent<ThunderEvent>().Init();
    }

}
