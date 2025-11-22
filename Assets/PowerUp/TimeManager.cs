using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    private float defaultFixedDeltaTime;
    private Coroutine currentSlowMo;

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

        Debug.Log("Waktu Melambat: " + startSlowFactor);

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;

            float progress = timer / duration;
            float currentScale = Mathf.Lerp(startSlowFactor, 1f, progress);

            Time.timeScale = currentScale;

            Time.fixedDeltaTime = defaultFixedDeltaTime * currentScale;

            yield return null;
        }

        Time.timeScale = 1f;
        Time.fixedDeltaTime = defaultFixedDeltaTime;
        currentSlowMo = null;

        Debug.Log("Waktu Normal");
    }
}