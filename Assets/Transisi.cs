using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

public class Transisi : MonoBehaviour
{
    public Animator TransitionFade;
    public Animator TransitionStart;

    public float transitiononTime = 1f;

    public static Transisi instance;

    //public PlayerMovement playerScript;

    public void Start()
    {
        instance = this;
        Debug.Log("Transisi instance set: " + instance);
    }

    //private IEnumerator BukaLayarDelay()
    //{
    //    // Tunggu 1 frame
    //    yield return null;

    //    if (Transition != null)
    //    {
    //        Transition.SetTrigger("Terangkan");
    //        Debug.Log("Terangi dunia ini nak.");
    //    }
    //}

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

    //public IEnumerator AktifkanTransisi(string sceneName, Animator transition, string trigger)
    //{
    //    if (transition != null)
    //    {
    //        transition.SetTrigger(trigger);
    //        Debug.Log("Mambo Transisi");
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Transition Animator not assigned!");
    //    }

    //    yield return new WaitForSecondsRealtime(transitiononTime);

    //    ////playerScript.isDead = false;
    //    SceneManager.LoadScene(sceneName);
    //    Debug.Log("Currently playing" + transition + trigger);
    //    ScoreManager.instance.ResetScore();


    //}

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

        yield return new WaitForSecondsRealtime(transitiononTime);

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

        // 6. Reset Player (Mencari player di scene secara otomatis lalu menghidupkannya)
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


    ////////public IEnumerator LooadLevel(string sceneName)
    ////////{
    ////////    if (Transition != null)
    ////////    {
    ////////        Transition.SetTrigger("Start");
    ////////        Debug.Log("Mambo Transisi");
    ////////    }
    ////////    else
    ////////    {
    ////////        Debug.LogWarning("Transition Animator not assigned!");
    ////////    }

    ////////    // Gunakan Realtime juga di sini
    ////////    //yield return new WaitForSecondsRealtime(transitiononTime);

    ////////    ////playerScript.isDead = false;
    ////////    //Time.timeScale = 1f;
    ////////    yield return new WaitForSeconds(transitiononTime);
    ////////    SceneManager.LoadScene(sceneName);
    ////////    Debug.Log("Loading scene by name: " + sceneName);
    ////////}

    //public IEnumerator LooadLevelStart(string sceneName)
    //{
    //    if (TransitionStart != null)
    //    {
    //        TransitionStart.SetTrigger("Start");
    //        Debug.Log("Mambo Transisi");
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Transition Animator not assigned!");
    //    }

    //    // Gunakan Realtime juga di sini
    //    //yield return new WaitForSecondsRealtime(transitiononTime);

    //    ////playerScript.isDead = false;
    //    //Time.timeScale = 1f;
    //    yield return new WaitForSeconds(transitiononTime);
    //    SceneManager.LoadScene(sceneName);
    //    Debug.Log("Loading scene by name: " + sceneName);
    //}

}