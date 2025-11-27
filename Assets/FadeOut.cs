using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;

    public void StartFadeOut(string nextScene)
    {
        StartCoroutine(Fade(nextScene));
    }

    private System.Collections.IEnumerator Fade(string scene)
    {
        Color c = fadeImage.color;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 1f, t / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }

        SceneManager.LoadScene(scene);        
    }
}
