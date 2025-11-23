using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        if (masterSlider != null && myMixer != null)
        {
            masterSlider.onValueChanged.AddListener(SetMasterVolume);
            if (PlayerPrefs.HasKey("masterVolume"))
            {
                LoadMasterVolume();
            }
            else
            {
                SetMasterVolume(masterSlider.value);
            }
        }

        if (bgmSlider != null && myMixer != null)
        {
            bgmSlider.onValueChanged.AddListener(SetBGMVolume);
            if (PlayerPrefs.HasKey("bgmVolume"))
            {
                LoadBGMVolume();
            }
            else
            {
                SetBGMVolume(bgmSlider.value);
            }
        }

        if (sfxSlider != null && myMixer != null)
        {
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
            if (PlayerPrefs.HasKey("sfxVolume"))
            {
                LoadSFXVolume();
            }
            else
            {
                SetSFXVolume(sfxSlider.value);
            }
        }
    }

    public void SetMasterVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0.0001f, 1f);
        if (myMixer != null)
        {
            myMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("masterVolume", volume);
            Debug.Log("Master Volume set to: " + volume);
        }
    }

    public void SetBGMVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0.0001f, 1f);
        if (myMixer != null)
        {
            myMixer.SetFloat("bgm", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("bgmVolume", volume);
            Debug.Log("BGM Volume set to: " + volume);
        }
    }

    public void SetSFXVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0.0001f, 1f);
        if (myMixer != null)
        {
            myMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("sfxVolume", volume);
            Debug.Log("SFX Volume set to: " + volume);
        }
    }

    private void LoadMasterVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
        SetMasterVolume(masterSlider.value);
    }

    private void LoadBGMVolume()
    {
        bgmSlider.value = PlayerPrefs.GetFloat("bgmVolume");
        SetBGMVolume(bgmSlider.value);
    }

    private void LoadSFXVolume()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        SetSFXVolume(sfxSlider.value);
    }
}
