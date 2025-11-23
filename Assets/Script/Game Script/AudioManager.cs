using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    ShieldBreak
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager AudioManagerInstance;

    [Header("AUDIO SOURCE")]
    public AudioSource source;

    [Header("BGM")]
    public AudioClip BGM;

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

    void Awake()
    {
        playBGM();
        AudioManagerInstance = this;
    }

    public void playBGM()
    {
        source.clip = BGM;
        source.loop = true;
        source.Play();
    }

    public void stopBGM()
    {
        source.Stop();
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



        }

        if (clip != null)
        {
            source.PlayOneShot(clip);
        }
    }
}
