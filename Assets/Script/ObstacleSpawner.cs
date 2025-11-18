using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject warning;
    private Vector3 warningPosition;
    // Obstacles
    public GameObject rock;
    public Vector3 rockPos = new Vector3(0,6.5f,-1);
    
    private int obstacleObject = 0; // 0 = rock, 1 = goat, 2 = bird, 3 = zeus, 4 = monkey
    private Vector3 obstaclePosition = Vector3.zero;



    public float spawnTime = 2;
    private float timer = 0;



    // Start is called before the first frame update
    void Start()
    {
    
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
            obstacleObject = Random.Range(0,1);

            //position utk obstacle
            switch (obstacleObject)
            {
                case 0:
                case 1:
                var pos_offset = Random.Range(0,3);
                obstaclePosition = rockPos + new Vector3(-2.5f + 2.5f * pos_offset,0,0);
                warningPosition = obstaclePosition - new Vector3(0,2,0);
                    break;
                case 2:
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
            Instantiate(warning,warningPosition,transform.rotation);
            break;
        case 2:
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
            break;
        case 2:
            break;
        case 3:
            break;
        }
    }
}
