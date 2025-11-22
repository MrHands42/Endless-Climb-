using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatObstacle : FallingObstacle
{
    void Start()
    {
        AudioManager.AudioManagerInstance.Play(SFX.Goat);
    }

    void Update()
    {
        transform.position += (Vector3.down * speed) * Time.deltaTime;

        if (transform.position.y < death)
        {
            Debug.Log("Destroyed Goat");
            Destroy(gameObject);
        }
    }
}
