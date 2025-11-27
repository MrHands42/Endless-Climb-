using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public enum SFX
{
    Rock,
    Goat,
    Bird,
    Monkey,
    Banana,
    Zeus,
    Lightning,
    ChangeGrid,
    Warning,
    WarningVariation,
    PowerUp,
    BackButton,
    EndScreen,
    GeneralButton,
    PlayButton,

    Dash,
    TimeSlow,
    Impact,
    Invincibility,
    Shield,
    ShieldBreak,

    Floor,
    Ambience,
    Exclamate,
    Whistle,
    FallScream
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager AudioManagerInstance;

    public float maxPitch;

    [Header("AUDIO SOURCES")]
    public AudioSource bgmSource; 
    public AudioSource sfxSource;  

    [Header("AUDIO MIXER")]
    public AudioMixer audioMixer; 
    public AudioMixerGroup bgmMixerGroup; 
    public AudioMixerGroup sfxMixerGroup;  

    [Header("BGM")]
    public AudioClip BGM;
    public AudioClip MainMenu;

    [Header("AUDIO GAMPELAY")]
    public AudioClip rockAudio;
    public AudioClip goatAudio;
    public AudioClip birdAudio;
    public AudioClip monkeyAudio;
    public AudioClip bananaAudio;
    public AudioClip zeusAudio;
    public AudioClip lightningAudio;
    public AudioClip changeGrid;
    public AudioClip warning;
    public AudioClip warningVariation;
    public AudioClip powerUp;

    public AudioClip dash;
    public AudioClip dio_time_stop;
    public AudioClip impacts;
    public AudioClip invincibility;
    public AudioClip shield; 

    [Header("AUDIO UI")]
    public AudioClip backButton;
    public AudioClip endScreen;
    public AudioClip generalButton;
    public AudioClip playButton;
    public AudioClip shieldBreak;

    [Header("CUTSCENE")]
    public AudioClip ambience;
    public AudioClip floorcrack;
    public AudioClip exclamation;
    public AudioClip whistling;
    public AudioClip scream;

    private UnityEngine.SceneManagement.Scene currentScene;

    void Awake()
    {
        currentScene = SceneManager.GetActiveScene();

        if (bgmSource != null && bgmMixerGroup != null)
        {
            bgmSource.outputAudioMixerGroup = bgmMixerGroup;
        }
        if (sfxSource != null && sfxMixerGroup != null)
        {
            sfxSource.outputAudioMixerGroup = sfxMixerGroup;
        }

        if (currentScene.name == "MainMenu")// main menu
        {
            playBGM(MainMenu);
        } 
        else if (currentScene.name == "SampleScene") // main menu
        {
            playBGM(BGM);
        } 
        else if (currentScene.name == "Comic")
        {
            playBGM(ambience);
        }

        AudioManagerInstance = this;
    }

    public void playBGM(AudioClip music)
    {
        if (bgmSource != null && BGM != null)
        {
            bgmSource.clip = music;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    public void stopBGM()
    {
        if (bgmSource != null)
        {
            bgmSource.Stop();
        }
    }

    public void Play(SFX sfx)
    {
        AudioClip clip = null;

        switch (sfx)
        {
            case SFX.Rock: clip = rockAudio; break;
            case SFX.Goat: clip = goatAudio; break;
            case SFX.Bird: clip = birdAudio; break;
            case SFX.Monkey: clip = monkeyAudio; break;
            case SFX.Banana: clip = bananaAudio; break;
            case SFX.Zeus: clip = zeusAudio; break;
            case SFX.Lightning: clip = lightningAudio; break;
            case SFX.ChangeGrid: clip = changeGrid; break;
            case SFX.Warning: clip = warning; break;
            case SFX.WarningVariation: clip = warningVariation; break;
            case SFX.PowerUp: clip = powerUp; break;
            case SFX.BackButton: clip = backButton; break;
            case SFX.EndScreen: clip = endScreen; stopBGM(); break;
            case SFX.GeneralButton: clip = generalButton; break;
            case SFX.PlayButton: clip = playButton; break;

            case SFX.Dash: clip = dash; break;
            case SFX.TimeSlow: clip = dio_time_stop; break;
            case SFX.Impact: clip = impacts; break;
            case SFX.Invincibility: clip = invincibility; break;
            case SFX.Shield: clip = shield; break;
            case SFX.ShieldBreak: clip = shieldBreak; break;

            case SFX.Ambience: clip = ambience; break;
            case SFX.Floor: clip = floorcrack; break;
            case SFX.Exclamate: clip = exclamation; break;
            case SFX.Whistle: clip = whistling; break;
            case SFX.FallScream: clip = scream; break;
        }


        if (clip != null && sfxSource != null)
        {
            AudioSource instanceSource = gameObject.AddComponent<AudioSource>();
            instanceSource.outputAudioMixerGroup = sfxMixerGroup;
            instanceSource.clip = clip;
            if (clip != ambience && clip != floorcrack && clip != whistling && clip != scream && clip != exclamation)
            {

                instanceSource.pitch = Random.Range(1,maxPitch);

            }
            instanceSource.PlayOneShot(clip);
            Destroy(instanceSource,clip.length / instanceSource.pitch);
        }
    }


    public void SetBGMVolume(float volume)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20); 
        }
    }

    public void SetSFXVolume(float volume)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);  
        }
    }
}