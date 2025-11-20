using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class Warning : MonoBehaviour
{
    public string warningType = "null";
    public UnityEvent WarningGoneNormal;
    public UnityEvent WarningGoneBanana;
    public UnityEvent WarningGoneMonkey;
    public float flashTime = 0.01f;
    public float flashcount = 5f;
    private float flash = 0f;
    private float timer = 0;
    public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        WarningGoneNormal.AddListener(GameObject.FindGameObjectWithTag("ObstacleSpawner").GetComponent<ObstacleSpawner>().CreateObstacle);
        WarningGoneBanana.AddListener(GameObject.FindGameObjectWithTag("ObstacleSpawner").GetComponent<ObstacleSpawner>().CreateBanana);
        WarningGoneMonkey.AddListener(GameObject.FindGameObjectWithTag("ObstacleSpawner").GetComponent<ObstacleSpawner>().CreateMonkey);
        sprite.color = new Color(255,255,255,255);
        flash = 0;
    }

    void Update()
    {
        if (timer < flashTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if (sprite.color.a == 255)
            {
                sprite.color = new Color(255,255,255,0);
            }
            else
            {
                sprite.color = new Color(255,255,255,255);
            }
            flash += 0.5f;
            checkDestroy(warningType);
            timer = 0;
        }
    }

    void checkDestroy(string type)
    {
        if (flash == flashcount)
        {
            if (type == "goat" || type == "rock" || type == "bird")
            {
                WarningGoneNormal.Invoke();
            }
            else if (type == "banana")
            {
                WarningGoneBanana.Invoke();

            }
            else if (type == "monkey")
            {
                WarningGoneMonkey.Invoke();
            }
            Debug.Log("Warning Destroyed");
            Destroy(gameObject); 
        }
    }
}
