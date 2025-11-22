using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager Instance;
    public static bool isPaused = false;

    [Header("UI References")]
    public GameObject pauseMenuUI;
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
            print("WTF DUDE");
            Destroy(gameObject);
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        AudioManager.AudioManagerInstance.Play(SFX.BackButton);
        Time.timeScale = 0f;
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
        }
        Cursor.visible = true;
    }

    private void ResumeGame()
    {
        AudioManager.AudioManagerInstance.Play(SFX.GeneralButton);
        Time.timeScale = 1f;
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
    }

    public void Restart()
    {
        AudioManager.AudioManagerInstance.Play(SFX.GeneralButton);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        pauseMenuUI.SetActive(false);
        gameOverUI.SetActive(false);
    }

    public void BackToMenu()
    {
        AudioManager.AudioManagerInstance.Play(SFX.BackButton);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        pauseMenuUI.SetActive(false);
        gameOverUI.SetActive(false);
    }
}