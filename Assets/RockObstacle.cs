using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockObstacle : MonoBehaviour
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
            Debug.Log("Destroyed ROCK");
            Destroy(gameObject);
        }
    }
}
