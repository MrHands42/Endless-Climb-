using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockObstacle : FallingObstacle
{
    void Start()
    {
        AudioManager.AudioManagerInstance.Play(SFX.Rock);
    }

    void Update()
    {
        transform.position += (Vector3.down * speed) * Time.deltaTime;
        transform.Rotate(0f, 0f, 100*speed * Time.deltaTime);

        if (transform.position.y < death)
        {
            Debug.Log("Destroyed Rock");
            Destroy(gameObject);
        }
    }
}
