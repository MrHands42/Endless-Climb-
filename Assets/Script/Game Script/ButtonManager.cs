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

    public PlayerMovement playerScript;

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

    public void TogglePauseGameplay()
    {
        if (pauseMenuUI != null)
        {
            bool isCurrentlyActive = pauseMenuUI.activeSelf;
            bool targetState = !isCurrentlyActive;

            pauseMenuUI.SetActive(targetState);

            // Berhentikan waktu total (0 = berhenti, 1 = jalan)
            Time.timeScale = targetState ? 0f : 1f;

            // Pastikan kursor muncul agar bisa klik tombol menu
            Cursor.visible = targetState;

            Debug.Log(targetState ? "Game Paused (Local)" : "Game Resumed (Local)");
        }
        TogglePause();
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
        //pauseMenuUI.SetActive(false);
        //gameOverUI.SetActive(false);


        isPaused = false;
        Transisi.instance.RestartScene();

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //Transisi.instance.LoadCurrentLevel();
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