using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _fxSlider;

    private float _musicVolume;
    private float _fxVolume;

    private void Start()
    {
        _fxVolume = PlayerPrefs.GetFloat("fxVolume");
        _fxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        _fxSlider.onValueChanged.AddListener(delegate{OnSFXVolumeChange();});
            
        _musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        _musicVolume = PlayerPrefs.GetFloat("musicVolume");
        _musicSlider.onValueChanged.AddListener(delegate{OnMusicVolumeChange();});
    }

    public void OnMusicVolumeChange()
    {
        _musicVolume = _musicSlider.value;
        PlayerPrefs.SetFloat("musicVolume", _musicVolume);
        PlayerPrefs.Save();
        Debug.Log($"{this.SoundManagerStamp()} musicVolume = " + PlayerPrefs.GetFloat("musicVolume"));
    }

    public void OnSFXVolumeChange()
    {
        _fxVolume = _fxSlider.value;
        PlayerPrefs.SetFloat("sfxVolume", _fxVolume);
        PlayerPrefs.Save();
        Debug.Log($"{this.SoundManagerStamp()} sfxVolume = " + PlayerPrefs.GetFloat("fxVolume"));
    }
}
