using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI References")]
    public GameObject gameOverUI;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        else
        {
            print("there is no game over ui");
        }
        AudioManager.AudioManagerInstance.Play(SFX.EndScreen);
        Debug.Log("Game Over!");
    }

    public void restartLevel()
    {
        Debug.Log("Restart button clicked - starting restart process");

        // Reset time scale agar game tidak beku
        Time.timeScale = 1f;
        Debug.Log("Time scale reset to 1");

        // Play SFX jika ada
        //if (AudioManager.AudioManagerInstance != null)
        //{
        //    AudioManager.AudioManagerInstance.Play(SFX.Click);
        //    Debug.Log("Restart SFX played");
        //}

        // Coba load via Transisi jika ada (untuk scene transisi)
        if (Transisi.instance != null)
        {
            Debug.Log("Loading level via Transisi.instance.LoadCurrentLevel()");
            Transisi.instance.LoadCurrentLevel();
        }
        else
        {
            Debug.LogWarning("Transisi.instance is null, falling back to direct load");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        Debug.Log("Restart process completed");
    }
}