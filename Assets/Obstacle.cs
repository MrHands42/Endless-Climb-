using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


