using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaObstacle : VectorObstacle
{
    protected void Update()
    {
        base.Update();
        transform.Rotate(0f, 0f, 360 * Time.deltaTime);
    }
}
