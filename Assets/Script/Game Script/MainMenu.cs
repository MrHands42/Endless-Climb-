using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject tutorialPanel;    
    public GameObject settingPanel;

    public void GameStart()
    {
        // Play Play Button Sound
        if (AudioManager.AudioManagerInstance != null)
        {
            AudioManager.AudioManagerInstance.Play(SFX.PlayButton);
        }
        else
        {
            Debug.LogError("AudioManagerInstance is null!");
        }
        
        SceneManager.LoadScene("GameScene");
        ScoreManager.instance.ResetScore();
    }

    public void QuitGame()
    {
        AudioManager.AudioManagerInstance.Play(SFX.BackButton);
        Application.Quit();
    }

    public void OpenTutorial()
    {
        AudioManager.AudioManagerInstance.Play(SFX.GeneralButton);
        tutorialPanel.SetActive(true);
    }

    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
        AudioManager.AudioManagerInstance.Play(SFX.BackButton);
    }
    
    public void OpenOption()
    {
        settingPanel.SetActive(true);
        AudioManager.AudioManagerInstance.Play(SFX.GeneralButton);
    }

    public void CloseSetting()
    {
        settingPanel.SetActive(false);
        AudioManager.AudioManagerInstance.Play(SFX.BackButton);
    }

    public void OpenCutscene()
    {
        AudioManager.AudioManagerInstance.Play(SFX.GeneralButton);
        SceneManager.LoadScene("Comic");
    }
}