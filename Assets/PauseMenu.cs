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
        if (!gameOverUI.activeSelf)
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
    }

    private void PauseGame()
    {
        AudioManager.AudioManagerInstance.Play(SFX.BackButton);
        Time.timeScale = 0f;
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
        }
        else
        {
            print("I DONT SEE SHIT");
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
        else
        {
            print("I DONT SEE SHIT");
        }
    }

    public void Restart()
    {
        AudioManager.AudioManagerInstance.Play(SFX.GeneralButton);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        pauseMenuUI.SetActive(false);
        gameOverUI.SetActive(false);
        ScoreManager.instance.ResetScore();
    }

    public void BackToMenu()
    {
        AudioManager.AudioManagerInstance.Play(SFX.BackButton);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        pauseMenuUI.SetActive(false);
        gameOverUI.SetActive(false);
        ScoreManager.instance.ResetScore();
    }
}