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

    public Animator MainMenuAni;
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

        // 1. Bekukan waktu
        Time.timeScale = 0f;

        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
            if (MainMenuAni != null) MainMenuAni.SetTrigger("Down");
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

        if (pauseMenuUI != null && MainMenuAni != null)
        {
            MainMenuAni.SetTrigger("Up");
            StartCoroutine(MatikanPanelDelay(0.5f));
        }
    }

    private System.Collections.IEnumerator MatikanPanelDelay(float delayWaktu)
    {
        yield return new WaitForSeconds(delayWaktu);

        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
    }

    public void Restart()
    {
        AudioManager.AudioManagerInstance.Play(SFX.GeneralButton);

        isPaused = false;
        //if (pauseMenuUI != null) pauseMenuUI.SetActive(false);
        //if (gameOverUI != null) gameOverUI.SetActive(false);

        Transisi.instance.RestartScene();
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