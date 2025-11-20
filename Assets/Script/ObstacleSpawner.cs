using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObstacleSpawner : MonoBehaviour
{
    // Sizes
    public float distance_box = 2.5f; // jarak antar kotak
    public float outside_box_y = 6.5f; // keluar screen (y axis)
    public float outside_box_x = 10f; // keluar screen (x axis)

    // Warning
    public GameObject warning;
    private Vector3 warningPosition;

    // Obstacles: vertical (vertobs)
    public GameObject rock;
    public GameObject goat;
    private Vector3 vertObsPos = Vector3.zero;
    public float vertObsWarning_offset = 2;

    // Obstacles:bird
    public GameObject bird;
    private Vector3 BirdPos = Vector3.zero;
    public float BirdWarning_offset = 3.5f;
    
    // other shit
    private int obstacleObject = 0; // 0 = rock, 1 = goat, 2 = bird, 3 = zeus, 4 = monkey
    private Vector3 obstaclePosition = Vector3.zero;



    public float spawnTime = 2;
    private float timer = 0;



    // Start is called before the first frame update
    void Start()
    {
        vertObsPos = new Vector3(0,outside_box_y,-1); 
        BirdPos = new Vector3(0,0,-1); 
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnTime)
        {
            timer += Time.deltaTime;
        }
        else
        {   
            obstacleObject = Random.Range(0,3);

            //position utk obstacle
            switch (obstacleObject)
            {
                //rock dan goat
                case 0:
                case 1:
                    var pos_offset = Random.Range(0,3);
                    obstaclePosition = vertObsPos + new Vector3(-distance_box + distance_box * pos_offset,0,0);
                    warningPosition = obstaclePosition - new Vector3(0,vertObsWarning_offset,0);
                    break;
                case 2:
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
                    
                    break;
                case 3:
                    break;
            }
            CreateWarning();
        }

    }

    void CreateWarning()
    {
        Debug.Log("Warning made");
        switch (obstacleObject)
        {
        case 0:
        case 1:
        case 2:
            Instantiate(warning,warningPosition,transform.rotation);
            break;
        case 3:
            break;
        }
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
        case 3:
            break;
        }
    }
}
