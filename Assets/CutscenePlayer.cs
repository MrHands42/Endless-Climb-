using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutscenePlayer : MonoBehaviour
{
    private int cutsceneIndex = 1;
    public GameObject cutscene1;
    public GameObject cutscene2;
    public GameObject cutscene3;
    public GameObject cutscene4;
    public GameObject button;
    public GameObject videoPlayer;

    private GameObject[] cutscenes;

    // Start is called before the first frame update
    void Start()
    {
        cutscenes = new GameObject[4];
        cutscene1.SetActive(true);

        cutsceneIndex = 1;
        cutscenes[0] = cutscene1;
        cutscenes[1] = cutscene2;
        cutscenes[2] = cutscene3;
        cutscenes[3] = cutscene4;
        AudioManager.AudioManagerInstance.Play(SFX.Whistle);
    }

    // Update is called once per frame
    public void playCutscene()
    {
        print("PRESSED");
        cutscenes[cutsceneIndex-1].SetActive(false);
        cutscenes[cutsceneIndex].SetActive(true);
        cutsceneIndex += 1;

        switch (cutsceneIndex)
        {
        case 2: AudioManager.AudioManagerInstance.Play(SFX.Exclamate); break;
        case 3: AudioManager.AudioManagerInstance.Play(SFX.Floor); break;
        case 4: 
            button.SetActive(false);
            VideoPlayer vp = videoPlayer.GetComponent<VideoPlayer>();
            vp.loopPointReached += OnVideoFinished;
            AudioManager.AudioManagerInstance.Play(SFX.FallScream); 
            vp.Play();
            break;
        }
    }

    public void test()
    {
        Debug.Log("PRESSED");
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        GetComponent<FadeOut>().StartFadeOut("MainMenu");
    }
}
