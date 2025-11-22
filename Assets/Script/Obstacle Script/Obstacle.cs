using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class FallingObstacle : MonoBehaviour
{
    public float speed = 5;
    public float death = -6.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3.down * speed) * Time.deltaTime;

        if (transform.position.y < death)
        {
            Debug.Log("Destroyed " + gameObject.name);
            Destroy(gameObject);
        }
    }
}

public class HorizontalObstacle : MonoBehaviour
{
    public float speed = 12;
    public float death = 10f;
    protected int Obsdirection = 1;
    // Start is called before the first frame update
    void Start()
    {
        if (transform.position.x < 0) // spawn left arah kanan
        {
            Obsdirection = 0;
        }
        else if (transform.position.x > 0) // spawn right arah kiri
        {
            Obsdirection = 1;
        }
        Debug.Log(Obsdirection + " is my dir");
    }

    // Update is called once per frame
    void Update()
    {
        if (Obsdirection == 0) // right
        {
            transform.position += (Vector3.right * speed) * Time.deltaTime;
            if (transform.position.x > death)
            {
                Debug.Log("Destroyed " + gameObject.name);
                Destroy(gameObject);
            }
        }
        else if (Obsdirection == 1)// left
        {
            transform.position += (Vector3.left * speed) * Time.deltaTime;
            if (transform.position.x < -death)
            {
                Debug.Log("Destroyed " + gameObject.name);
                Destroy(gameObject);
            }
        }

    }
}

public class VectorObstacle : MonoBehaviour
{
    public float speed = 10f;
    public float death = -6.5f;
    public Vector3 direction = Vector3.zero;

    private GameObject tiles;
    private GameObject target;

    void Start()
    {

    }

    void Update()
    {
        transform.position += (direction * speed) * Time.deltaTime;

        if (transform.position.y < death)
        {
            Debug.Log("Destroyed " + gameObject.name);
            Destroy(gameObject);
        }
    }
}

public class MonkeyObstacle : MonoBehaviour
{
    public float deathTimer = 10f;
    private float deathCount = 0;

    public float throwInterval = 3;
    private float throwCount = 0;

    public UnityEvent ThrowBanana;


    public void Start()
    {
        throwCount = 2;
        ThrowBanana.AddListener(GameObject.FindGameObjectWithTag("ObstacleSpawner").GetComponent<ObstacleSpawner>().BananaSignal);
    }

    public void Update()
    {
        if (deathCount < deathTimer)
        {
            deathCount += Time.deltaTime;
        }
        else
        {
            Debug.Log("Destroyed" + gameObject.name);
            Destroy(gameObject);
        }
        
        if (throwCount < throwInterval)
        {
            throwCount += Time.deltaTime;
        }
        else
        {
            ThrowBanana.Invoke();
            throwCount = 0;
        }
    }
}

public class ZeusObstacle : MonoBehaviour
{
    public float deathTimer = 3f;
    private float deathCount = 0;
    public UnityEvent zeusStrike;

    public void Start()
    {
        zeusStrike.AddListener(GameObject.FindGameObjectWithTag("ObstacleSpawner").GetComponent<ObstacleSpawner>().LightningSignal);
    }

    public void Update()
    {
        if (deathCount < deathTimer)
        {
            deathCount += Time.deltaTime;
        }
        else
        {
            zeusStrike.Invoke();
            Debug.Log("Destroyed" + gameObject.name);
            Destroy(gameObject);
        }
    }
}

public class LightningObstacle : MonoBehaviour
{
    public float deathTimer = 0.5f;
    private float deathCount = 0;

    public void Start()
    {
       
    }

    public void Update()
    {
        if (deathCount < deathTimer)
        {
            deathCount += Time.deltaTime;
        }
        else
        {
            Debug.Log("Destroyed" + gameObject.name);
            Destroy(gameObject);
        }
    }
}