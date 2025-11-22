using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [Header("Obstacle Tags")]
    public List<string> obstacleTags = new List<string> { "Obstacle" };  // Add more if needed, e.g., "Spike"

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger entered with: " + collision.gameObject.name + " (Tag: " + collision.tag + ")");

        IItem item = collision.GetComponent<IItem>();
        if (item != null)
        {
            item.Collect();
            Debug.Log("Collected item!");
        }
        else if (obstacleTags.Contains(collision.tag))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
                Debug.Log("Game Over triggered!");
            }
            else
            {
                Debug.LogError("GameManager.Instance is null!");
            }
        }
        else
        {
            Debug.Log("No action for: " + collision.tag);
        }
    }
}