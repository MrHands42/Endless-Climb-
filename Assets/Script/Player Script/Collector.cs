using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [Header("Obstacle Tags")]
    public List<string> obstacleTags = new List<string> { "Obstacle" };  // Add more if needed, e.g., "Spike"

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger entered with: " + collision.gameObject.name + " (Tag: " + collision.tag + ")");

        IItem item = collision.GetComponent<IItem>();
        if (item != null)
        {
            item.Collect();
            Debug.Log("Collected item!");
            return;

        }
        else if (obstacleTags.Contains(collision.tag))
        {
            // Cek apakah ada invincibility apa engga
            if (isInvincible)
            {
                Debug.Log("Nabrak Obstacle, tapi aman aja lek.");
                return;
            }

            if (hasShield)
            {
                Debug.Log("Shield pecah cik");
                BreakShield();
                Destroy(collision.gameObject);
                return;
            }

            // --- JALUR KEMATIAN ---
            PlayerMovement mechanic = GetComponentInParent<PlayerMovement>();
            if (mechanic != null)
            {
                // Hentikan efek visual Collector (seperti Invincibility)
                StopAllCoroutines();

                // Suruh Induk mengeksekusi logika jatuh
                mechanic.RopeSlip();
            }
            else
            {
                Debug.LogError("JALUR B: Gawat! Script PlayerMovement TIDAK DITEMUKAN di Parent! (Langsung Freeze)");
                if (GameManager.Instance != null) GameManager.Instance.GameOver();
            }
        }

        else
        {
            Debug.Log("No action for: " + collision.tag);
        }
    }

    [Header("Invincibility Visuals")]
    public SpriteRenderer invincibilityRenderer;
    public bool isInvincible = false;

    [Header("Shield Visuals")]
    public GameObject shieldVisual; 
    public bool hasShield = false; 

    public void ActivateInvincibility(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(InvincibilityRoutine(duration));
    }

    public void ActivateShield()
    {
        hasShield = true;
        if (shieldVisual != null) shieldVisual.SetActive(true);
    }

    public void BreakShield()
    {
        hasShield = false;
        if (shieldVisual != null) shieldVisual.SetActive(false);


        if (AudioManager.AudioManagerInstance != null)
        {
            AudioManager.AudioManagerInstance.Play(SFX.ShieldBreak);
        }
    }

    IEnumerator InvincibilityRoutine(float duration)
    {
        isInvincible = true;

        if (invincibilityRenderer != null)
        {
            invincibilityRenderer.gameObject.SetActive(true);

            // Maxwell jadi putih
            Color c = invincibilityRenderer.color;
            c.a = 0.5f;
            invincibilityRenderer.color = c;
        }

        Debug.Log("Invincibility Aktif!");

        // Fading out Maxwell
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;

            if (invincibilityRenderer != null)
            {
                float progress = timer / duration;
                float newAlpha = Mathf.Lerp(0.5f, 0f, progress);

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