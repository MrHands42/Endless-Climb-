using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class ObstacleSpawner : MonoBehaviour
{
    private GameObject player;

    [Header("Grid Componenets")]
    // Sizes
    public float distance_box = 2.5f;    // jarak antar kotak
    public float outside_box_y = 6.5f;   // keluar screen (y axis)
    public float outside_box_x = 10f;    // keluar screen (x axis)

    [Header("Warning")]
    // Warning
    public GameObject warning;
    private Vector3 warningPosition;

    [Header("Verical Obstacles")]
    // Obstacles: vertical (vertobs)
    public GameObject rock;
    public GameObject goat;
    private Vector3 vertObsPos = Vector3.zero;
    public float vertObsWarning_offset = 2;

    [Header("Horizontal Obstacles")]
    // Obstacles: bird
    public GameObject bird;
    private Vector3 BirdPos = Vector3.zero;
    public float BirdWarning_offset = 3.5f;
    

    [Header("Monkey Obstacles")]
    // Obstacles: monkey
    public GameObject monkey;
    public GameObject banana;
    public float monkeySpawn = 40;
    private float monkeyTimer = 10; // monkey cooldown
    private Vector3 bananaDirection = Vector3.zero;
    private Vector3 MonkeyPos = Vector3.zero;

    public GameObject tiles;
    private GameObject target;

    [Header("Zeus Obstacle")]
    public GameObject zeus;
    public GameObject strike;
    public float zeusSpawn = 55;
    private float zeusTimer = 10;
    private Vector3 ZeusPos = new Vector3(0,0,-1);
    private Vector3 strikePos = new Vector3(0,0,-1);
    private Dictionary<int,Vector3> strikeDict = new Dictionary<int, Vector3>();
    private bool strikeMade = false;

    // other shit
    private int obstacleObject = 0; // 0 = rock, 1 = goat, 2 = bird, 3 = monkey, 4 = banana
    private Vector3 obstaclePosition = Vector3.zero;
    private int obstaclePool = 4;
    private GameObject warningClone;

    [Header("Spawn Rate")]
    public float spawnTime = 10;
    public float slowSpawn = 4f;
    public float fastSpawn = 0.5f;
    public float speedUpDuration = 60f;
    private float timer = 0;

    private float debugTimer = 0;


    // Start is called before the first frame update
    void Start()
    {
        strikeDict[1] = new Vector3(0,0,-1);
        strikeDict[2] = new Vector3(0,0,-1);
        player = GameObject.FindGameObjectWithTag("Player");
        obstaclePool = 3;
        vertObsPos = new Vector3(0,outside_box_y,-1); 
        BirdPos = new Vector3(0,0,-1); 
        MonkeyPos = new Vector3(0,4.5f,-1);
    }

    // Update is called once per frame
    void Update()
    {
        float t = Mathf.Clamp01(Time.timeSinceLevelLoad / speedUpDuration);
        spawnTime = Mathf.Lerp(slowSpawn, fastSpawn, t);
        ZeusCooldown();
        MonkeyCooldown();
        if (timer < spawnTime)
        {
            // debugTimer += Time.deltaTime;
            timer += Time.deltaTime;
        }
        else
        {   
            obstacleObject = Random.Range(0,obstaclePool);
            //position utk obstacle
            switch (obstacleObject)
            {
                //rock dan goat
                case 0:
                    var pos_offset = Random.Range(0,3);
                    obstaclePosition = vertObsPos + new Vector3(-distance_box + distance_box * pos_offset,0,0);
                    warningPosition = obstaclePosition - new Vector3(0,vertObsWarning_offset,0);
                    CreateWarning("rock");
                    break;
                case 1:
                    pos_offset = Random.Range(0,3);
                    obstaclePosition = vertObsPos + new Vector3(-distance_box + distance_box * pos_offset,0,0);
                    warningPosition = obstaclePosition - new Vector3(0,vertObsWarning_offset,0);
                    CreateWarning("goat");
                    break;
                case 2: //BIRD
                    pos_offset = Random.Range(0,3);
                    var dir = Random.Range(0,2);

                    if (dir == 0) //left
                    {
                    obstaclePosition = BirdPos + new Vector3(-outside_box_x, -distance_box + distance_box * pos_offset,0);
                    warningPosition = obstaclePosition + new Vector3(BirdWarning_offset,0,0);
                    }
                    else if (dir == 1) // right
                    {
                    obstaclePosition = BirdPos + new Vector3(outside_box_x, -distance_box + distance_box * pos_offset,0);
                    warningPosition = obstaclePosition - new Vector3(BirdWarning_offset,0,0);
                    }
                    CreateWarning("bird");
                    break;
                case 3: //Monkey
                    break;
            }
        }

    }

    public void CreateWarning(string obstacleType)
    {
        Debug.Log("Warning made");
        warningClone = Instantiate(warning,warningPosition,transform.rotation);
        warningClone.GetComponent<Warning>().warningType = obstacleType;
        timer = 0;
    }

    public void CreateObstacle()
    {
        switch (obstacleObject)
        {
        case 0:
            Debug.Log("Rock Made");
            Instantiate(rock,obstaclePosition,transform.rotation);
            break;
        case 1:
            Debug.Log("Goat Made");
            Instantiate(goat,obstaclePosition,transform.rotation);
            break;
        case 2:
            Debug.Log("Bird Made");
            Instantiate(bird,obstaclePosition,transform.rotation);
            break;
        }
    }

    public void CreateBanana()
    {
        Debug.Log("Banana Made");
        GameObject bananaInstance = Instantiate(banana,MonkeyPos,transform.rotation);
        bananaInstance.GetComponent<BananaObstacle>().direction = bananaDirection;
    }    

    public void CreateMonkey()
    {
        Debug.Log("Monkey Made");
        GameObject monkeyInstance = Instantiate(monkey,MonkeyPos,transform.rotation);
    }

    public void MonkeyCooldown()
    {
        if (monkeyTimer < monkeySpawn)
        {
            monkeyTimer += Time.deltaTime;
        }
        else
        {
            var pos_offset = Random.Range(0,3);
            MonkeyPos = new Vector3(-distance_box + distance_box * pos_offset,4.5f,-1);
            warningPosition = MonkeyPos;

            GameObject instance = Instantiate(warning,warningPosition,transform.rotation);
            instance.GetComponent<Warning>().warningType = "monkey";

            monkeyTimer = 0;
        }
    }

    public void BananaSignal()
    {   
        tiles = GameObject.FindGameObjectWithTag("Tiles");

        float nearest = 1000000000;
        GameObject closestTile = null; 

        for (int i = 0; i < tiles.transform.childCount; i++)
        {
            GameObject tile = tiles.transform.GetChild(i).gameObject;
            float distance = Vector3.Distance(player.transform.position, tile.transform.position);

            if (distance < nearest)
            {
                nearest = distance;
                closestTile = tile;
            }
        }

        // int randomTile = Random.Range(0, tiles.transform.childCount);
        // target = tiles.transform.GetChild(randomTile).gameObject;

        Vector3 targetPos = closestTile.transform.position;
        bananaDirection = (targetPos - MonkeyPos).normalized;

        GameObject instance = Instantiate(warning,targetPos,transform.rotation);
        instance.GetComponent<Warning>().warningType = "banana";
    }


    public void CreateZeus()
    {
        strikeMade = false;
        Debug.Log("Zues Made");
        GameObject zeusInstance = Instantiate(zeus,ZeusPos,transform.rotation);
    }

    public void ZeusCooldown()
    {
        if (zeusTimer < zeusSpawn)
        {
            zeusTimer += Time.deltaTime;
        }
        else
        {
            GameObject instance = Instantiate(warning,ZeusPos,transform.rotation);
            instance.GetComponent<Warning>().warningType = "zeus";

            zeusTimer = 0;
        }
    }

    public void CreateStrike()
    {
        if (!strikeMade)
        {
            for (int i = 1; i < 3; i++)
            {
                Debug.Log("Strike Made");
                GameObject strikeInstance = Instantiate(strike,strikeDict[i] - new Vector3(0,distance_box,0),transform.rotation);
            }
        }
        strikeMade = true;
    }

    public void LightningSignal()
    {
        int randomNum = Random.Range(0,3);
        int randomNum2 = Random.Range(0,3);
        while (randomNum2 == randomNum)
        {
            randomNum2 = Random.Range(0,3);
        }

        strikeDict[1] = new Vector3(-distance_box + distance_box*randomNum,distance_box,-1);
        strikeDict[2] = new Vector3(-distance_box + distance_box*randomNum2,distance_box,-1);
        
        for (int i = 0; i < 3; i++)
        {
            GameObject instance = Instantiate(warning,strikeDict[1] - new Vector3(0,distance_box*i),transform.rotation);
            instance.GetComponent<Warning>().warningType = "strike";
            instance.GetComponent<Warning>().flashcount = 10f;
        }
        
        for (int i = 0; i < 3; i++)
        {
            GameObject instance = Instantiate(warning,strikeDict[2] - new Vector3(0,distance_box*i),transform.rotation);
            instance.GetComponent<Warning>().warningType = "strike";
            instance.GetComponent<Warning>().flashcount = 10f;
        }
    }
}
