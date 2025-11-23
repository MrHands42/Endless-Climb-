using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    private float defaultFixedDeltaTime;
    private Coroutine currentSlowMo;

    [Header("TimeSLow Visuals")]
    public SpriteRenderer timeSlowSprite;

    void Awake()
    {
        instance = this;
        defaultFixedDeltaTime = Time.fixedDeltaTime;
    }

    public void ActivateSlowMotion(float slowFactor, float duration)
    {
        // Hentikan manipulasi waktu yang sedang berjalan, jika ada
        if (currentSlowMo != null)
        {
            StopCoroutine(currentSlowMo);
        }

        currentSlowMo = StartCoroutine(GradualRecoveryRoutine(slowFactor, duration));
    }

    IEnumerator GradualRecoveryRoutine(float startSlowFactor, float duration)
    {
        Time.timeScale = startSlowFactor;
        Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;

        if (timeSlowSprite != null)
        {
            timeSlowSprite.gameObject.SetActive(true);
    
            Color c = timeSlowSprite.color;
            c.a = 1f;
            timeSlowSprite.color = c;
        }

        Debug.Log("Waktu Melambat: " + startSlowFactor);

        float timer = 0f;

        while (timer < duration)
        {

            if (Time.timeScale == 0)
            {
                yield return null;
                continue;
            }

            timer += Time.unscaledDeltaTime;

            float progress = timer / duration;
            float currentScale = Mathf.Lerp(startSlowFactor, 1f, progress);

            Time.timeScale = currentScale;
            Time.fixedDeltaTime = defaultFixedDeltaTime * currentScale;


            if (timeSlowSprite != null)
            {
                float currentAlpha = Mathf.Lerp(1f, 0f, progress);

                Color c = timeSlowSprite.color;
                c.a = currentAlpha;
                timeSlowSprite.color = c;
            }

            yield return null;
        }

        Time.timeScale = 1f;
        Time.fixedDeltaTime = defaultFixedDeltaTime;
        currentSlowMo = null;

        if (timeSlowSprite != null)
        {
            timeSlowSprite.gameObject.SetActive(false);
        }

        Debug.Log("Waktu Normal");
    }
}