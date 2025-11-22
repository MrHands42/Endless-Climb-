using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GameStart()
    {
        AudioManager.AudioManagerInstance.Play(SFX.PlayButton);
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        AudioManager.AudioManagerInstance.Play(SFX.BackButton);
        Application.Quit();
    }
}
