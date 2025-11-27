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
    public GameObject frame1;
    public GameObject frame2;
    public GameObject frame3;
    public GameObject frame4;
    public GameObject frame5;
    public GameObject frame6;
    public GameObject frame7;
    public GameObject frame8;
    public GameObject frame9;
    public GameObject frame10;
    public GameObject frame11;
    public GameObject frame12;
    public GameObject frame13;
    public GameObject button;

    private GameObject[] cutscenes;
    private GameObject[] frames;
    public float delay = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        cutscenes = new GameObject[4];
        frames = new GameObject[13];
        cutscene1.SetActive(true);

        cutsceneIndex = 1;
        cutscenes[0] = cutscene1;
        cutscenes[1] = cutscene2;
        cutscenes[2] = cutscene3;

        frames[0] = frame1;
        frames[1] = frame2;
        frames[2] = frame3;
        frames[3] = frame4;
        frames[4] = frame5;
        frames[5] = frame6;
        frames[6] = frame7;
        frames[7] = frame8;
        frames[8] = frame9;
        frames[9] = frame10;
        frames[10] = frame11;
        frames[11] = frame12;
        frames[12] = frame13;

        AudioManager.AudioManagerInstance.Play(SFX.Whistle);
    }

    // Update is called once per frame
    public void playCutscene()
    {
        print("PRESSED");
        cutscenes[cutsceneIndex-1].SetActive(false);
        if (cutsceneIndex < 3)
        {
            cutscenes[cutsceneIndex].SetActive(true);
        }

        cutsceneIndex += 1;

        switch (cutsceneIndex)
        {
        case 2: AudioManager.AudioManagerInstance.Play(SFX.Exclamate); break;
        case 3: AudioManager.AudioManagerInstance.Play(SFX.Floor); break;
        case 4: playVideoManually();break;
        }
    }

    public void test()
    {
        Debug.Log("PRESSED");
    }
    void playVideoManually()
    {
        button.SetActive(false);
        AudioManager.AudioManagerInstance.Play(SFX.FallScream);
        StartCoroutine(PlayFrames());
    }

    IEnumerator PlayFrames()
    {
        for (int i = 0; i < frames.Length; i++)
        {
            if (i > 0)
            {
                frames[i - 1].SetActive(false);
            }

            frames[i].SetActive(true);

            yield return new WaitForSeconds(delay); 
        }

        GetComponent<FadeOut>().StartFadeOut("MainMenu");
    }
    }
