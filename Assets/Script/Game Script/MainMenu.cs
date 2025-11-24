     using System.Collections;
     using System.Collections.Generic;
     using UnityEngine;
     using UnityEngine.SceneManagement;
     public class MainMenu: MonoBehaviour
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
             Debug.Log("OpenTutorial called"); 
             if (tutorialPanel != null)
             {
                 tutorialPanel.SetActive(true);
                 Debug.Log("Tutorial panel activated"); 
             }
             else
             {
                 Debug.LogError("tutorialPanel is null! Assign it in Inspector."); 
             }
         }
         public void CloseTutorial()
         {
             Debug.Log("CloseTutorial called");
             if (tutorialPanel != null)
             {
                 tutorialPanel.SetActive(false);
             }
             else
             {
                 Debug.LogError("tutorialPanel is null!");
             }
         }
     }