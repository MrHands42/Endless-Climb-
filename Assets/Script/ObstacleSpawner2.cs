using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class ObstacleSpawner2 : MonoBehaviour
{
    private GameObject player;

    [Header("Grid Componenets")]
    // Sizes
    public float distance_box = 2.5f; // jarak antar kotak
    public float outside_box_y = 6.5f; // keluar screen (y axis)
    public float outside_box_x = 10f; // keluar screen (x axis)

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
    // Obstacles:bird
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


    // other shit
    private int obstacleObject = 0; // 0 = rock, 1 = goat, 2 = bird, 3 = monkey, 4 = banana
    private Vector3 obstaclePosition = Vector3.zero;
    private int obstaclePool = 4;
    private GameObject warningClone;


    public float spawnTime = 2;
    private float timer = 0;

    private float debugTimer = 0;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        obstaclePool = 3;
        vertObsPos = new Vector3(0,outside_box_y,-1); 
        BirdPos = new Vector3(0,0,-1); 
        MonkeyPos = new Vector3(0,4.5f,-1);
    }

    // Update is called once per frame
    void Update()
    {
        MonkeyCooldown();
        if (timer < spawnTime)
        {
            // debugTimer += Time.deltaTime;
            Debug.Log("Timer: " + debugTimer);
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
            obstaclePosition = MonkeyPos;
            warningPosition = obstaclePosition;

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

}
