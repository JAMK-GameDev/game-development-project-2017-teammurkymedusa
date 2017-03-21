using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {

    //These are going to be controlled by difficulty modifier
    public int[] WindBurstsAmount; // 0th index is min and 1st index is max
    public int[] BirdsAmount; // 0th index is min and 1st index is max

    [Range(1,10)]
    public int Difficulty = 1; // Controls occurence of the obstacles

    public int seed = 127326543;

    public int LandHeight = 1; // Adds to center of the map
    public int SkyHeight; // Adds to center of the map

    public int LevelHeight;
    public int LevelLength;

    public GameObject GroundMaterial;
    public GameObject Level;
    public GameObject Sky;
    public GameObject Ground;
    //Private variables
    GameObject[,] GroundArray;
    GameObject[] Events; //Stores random events such as wind and birds
    System.Random pseudoRandom;
    GameObject StartingPointObject;
    GameObject EndPoint;

    
	// Use this for initialization
	void Start () {
        int difficultyMod = defineDifficultyMod(Difficulty);
        pseudoRandom = new System.Random(seed);

        WindBurstsAmount = new int[2];
        BirdsAmount = new int[2];

        //Mins and maxes for events

        WindBurstsAmount[0] = pseudoRandom.Next(Difficulty, difficultyMod);
        WindBurstsAmount[1] = pseudoRandom.Next(difficultyMod, LevelLength * 3);

        BirdsAmount[0] = pseudoRandom.Next(Difficulty, difficultyMod);
        BirdsAmount[1] = pseudoRandom.Next(difficultyMod, LevelLength * 3);
        //pseudoRandom.Next();
        GroundArray = new GameObject[LevelLength,LandHeight];
        generateMap();
	}
	
    private int defineDifficultyMod(int difficulty)
    {
        if (difficulty == 1)
        {
            return 2;
        }
        else if (difficulty > 1 && difficulty < 5)
        {
            return difficulty / 2;
        }
        else if (difficulty > 5 && difficulty < 8)
        {
            return difficulty / 2;
        }
        else if (difficulty > 8)
        {
            return difficulty;
        }
        return 1;
    }
	// Update is called once per frame
	void Update () {
	
	}

    private void generateMap()
    {
        Debug.Log("Generating ground");
        generateGround();
    }

    private void generateGround()
    {
        bool enableMultiLayer = true;
        Vector2 startingPoint = new Vector2(0, 0);
        startingPoint.y -= GroundMaterial.GetComponent<SpriteRenderer>().bounds.size.y;

        //Do we need more than one layer of ground tiles?
        if(LandHeight >=  2 && enableMultiLayer)
        {
            Vector3 genPos = startingPoint;

            for (int x = 0; x <= LevelLength; x++)
            {
                if(x != 0)
                {
                    genPos.x += GroundMaterial.GetComponent<SpriteRenderer>().bounds.size.x;
                }

                for (int y = 0; y <= LevelHeight; y++)
                {
                    Debug.Log("Generating tile " + x + "," + y);
                    if (y != 0)
                    {
                        genPos.y += GroundMaterial.GetComponent<SpriteRenderer>().bounds.size.y;
                    }

                    GroundArray[x, y] = (GameObject)Instantiate(GroundMaterial, genPos, Quaternion.identity, Ground.transform);
                }
            }
        }
        //If not
        else
        {
            Vector3 genPos = startingPoint;
            for (int x = 0; x < LevelLength; x++)
            {
                Debug.Log("Generating tile " + x + ", 0" );
                if(x != 0)
                {
                    genPos.x += GroundMaterial.GetComponent<SpriteRenderer>().bounds.size.x;
                }
                GroundArray[x, 0] = (GameObject) Instantiate(GroundMaterial,genPos,Quaternion.identity, Ground.transform);

            }
        }
    }

    /// <summary>
    /// Generates Skybox and non-event things
    /// </summary>
    private void generateSky()
    {
        //Skybox Generation
        GameObject SkyboxCamera = GameObject.Find("Skybox Camera");
        Camera camera = SkyboxCamera.GetComponent<Camera>();
        GameObject BGImage = GameObject.Find("Background Image");
        GUITexture image = BGImage.GetComponent<GUITexture>();

        //image.texture = 

        //Generate coins and what have you
    }

    /// <summary>
    /// TODO: Missing actual events
    /// </summary>
    private void eventPopulation()
    {

    }
}
