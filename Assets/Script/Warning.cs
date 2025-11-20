using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class Warning : MonoBehaviour
{

    public UnityEvent WarningGone;
    public float flashTime = 0.01f;
    public float flashcount = 5f;
    private float flash = 0f;
    private float timer = 0;
    public SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        WarningGone.AddListener(GameObject.FindGameObjectWithTag("ObstacleSpawner").GetComponent<ObstacleSpawner>().CreateObstacle);
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
            checkDestroy();
            timer = 0;
        }
    }

    void checkDestroy()
    {
        if (flash == flashcount)
        {
            WarningGone.Invoke();
            Debug.Log("Warning Destroyed");
            Destroy(gameObject);
        }
    }
}
