using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

public class Transisi : MonoBehaviour
{
    public Animator TransitionFade;
    public Animator TransitionStart;

    public float transitionTimeFade = 1f;
    public float transitionTimeStart = 2f;

    public static Transisi instance;


    public void Start()
    {
        instance = this;
        Debug.Log("Transisi instance set: " + instance);
    }

    public void MasukGame()
    {
        AudioManager.AudioManagerInstance.Play(SFX.GeneralButton);
        StartCoroutine(AktifkanTransisi("GameScene", TransitionStart, "Mulai"));
    }

    public void RestartScene()
    {
        StartCoroutine(AktifkanTransisi(SceneManager.GetActiveScene().name, TransitionFade, "Gelapkan"));
    }

    public void BackToMenu()
    {
        AudioManager.AudioManagerInstance.Play(SFX.GeneralButton);
        StartCoroutine(AktifkanTransisi("MainMenu", TransitionFade, "Gelapkan"));
    }

    public IEnumerator AktifkanTransisi(string sceneName, Animator transition, string trigger)
    {
        if (transition != null)
        {
            transition.SetTrigger(trigger);
            Debug.Log("Mambo Transisi: " + trigger);
        }
        else
        {
            Debug.LogWarning("Transition Animator not assigned!");
        }

        if(transition == TransitionFade)
        {
            yield return new WaitForSecondsRealtime(transitionTimeFade);
        }
        else if(transition == TransitionStart)
        {
            yield return new WaitForSecondsRealtime(transitionTimeStart);
        }
        else
        {
            Debug.LogWarning("Unknown transition animator, using default wait time.");
            yield return new WaitForSecondsRealtime(1f);
        }


        Time.timeScale = 1f;

        if (ButtonManager.Instance != null)
        {
            ButtonManager.isPaused = false;
            if (ButtonManager.Instance.pauseMenuUI != null) ButtonManager.Instance.pauseMenuUI.SetActive(false);
            if (ButtonManager.Instance.gameOverUI != null) ButtonManager.Instance.gameOverUI.SetActive(false);
        }

        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.ResetScore();
        }

        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        if (player != null)
        {
            player.isDead = false;
        }

        SceneManager.LoadScene(sceneName);
        Debug.Log("Loading Scene: " + sceneName);

        if(trigger == "Gelapkan")
        {
            transition.SetTrigger("Terangkan");
        }
    }

}