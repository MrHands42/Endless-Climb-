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
            // Cek apakah ada invincibility apa engga
            if (isInvincible)
            {
                Debug.Log("Nabrak Obstacle, tapi aman aja lek.");
                return;
            }

            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
                Debug.Log("Game Over triggered!");
            }

            else
            {
                Time.timeScale = 0f;
                Debug.LogError("GameManager gaada cik");
            }
        }

        else
        {
            Debug.Log("No action for: " + collision.tag);
        }
    }

    [Header("Invincibility Visuals")]
    public SpriteRenderer invincibilityRenderer;
    private bool isInvincible = false;

    public void ActivateInvincibility(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(InvincibilityRoutine(duration));
    }

    IEnumerator InvincibilityRoutine(float duration)
    {
        isInvincible = true;

        if (invincibilityRenderer != null)
        {
            invincibilityRenderer.gameObject.SetActive(true);

            // Maxwell jadi putih
            Color c = invincibilityRenderer.color;
            c.a = 1f;
            invincibilityRenderer.color = c;
        }

        Debug.Log("Mode Dewa Aktif!");

        // Fading out Maxwell
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;

            if (invincibilityRenderer != null)
            {
                float progress = timer / duration;
                float newAlpha = Mathf.Lerp(1f, 0f, progress);

                Color c = invincibilityRenderer.color;
                c.a = newAlpha;
                invincibilityRenderer.color = c;
            }

            yield return null;
        }

        isInvincible = false;

        // Matikan Aura Maxwell
        if (invincibilityRenderer != null) invincibilityRenderer.gameObject.SetActive(false);
        Debug.Log("Invicibility habis");
    }
}