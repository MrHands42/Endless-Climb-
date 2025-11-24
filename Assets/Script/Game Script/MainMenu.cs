using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Tambahkan ini: Deklarasi variabel untuk panel tutorial
    public GameObject tutorialPanel;

    public void GameStart()
    {
        AudioManager.AudioManagerInstance.Play(SFX.PlayButton);
        SceneManager.LoadScene("SampleScene");
        ScoreManager.instance.ResetScore();
    }

    public void QuitGame()
    {
        AudioManager.AudioManagerInstance.Play(SFX.BackButton);
        Application.Quit();
    }

    public void OpenTutorial()
    {
        tutorialPanel.SetActive(true);
    }

    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
    }
}
