using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    private float _musicVolume;
    private float _fxVolume;

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private List<AudioSource> _SFXsources;

    private void Start()
    {
        _fxVolume = PlayerPrefs.GetFloat("fxVolume");
        _sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        
        SetSFXVolume();
        
        if(_sfxSlider != null)
            _sfxSlider.onValueChanged.AddListener(delegate{OnSFXValueChange();});

        _musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        _musicVolume = PlayerPrefs.GetFloat("musicVolume");
        
        SetMusicVolume();
        
        if(_musicSlider != null)
            _musicSlider.onValueChanged.AddListener(delegate{OnMusicValueChange();});
    }

    private void OnMusicValueChange()
    {
        _musicVolume = _musicSlider.value;
        PlayerPrefs.SetFloat("musicVolume", _musicVolume);
        PlayerPrefs.Save();
        Debug.Log($"{this.SoundManagerStamp()} musicVolume = " + PlayerPrefs.GetFloat("musicVolume"));
        
        SetMusicVolume();
    }

    private void SetMusicVolume()
    {
        _musicSource.volume = _musicVolume;
    }

    private void OnSFXValueChange()
    {
        _fxVolume = _sfxSlider.value;

        SetSFXVolume();
        
        PlayerPrefs.SetFloat("sfxVolume", _fxVolume);
        PlayerPrefs.Save();
        Debug.Log($"{this.SoundManagerStamp()} sfxVolume = " + PlayerPrefs.GetFloat("fxVolume"));
    }

    private void SetSFXVolume()
    {
        foreach (var source in _SFXsources)
        {
            source.volume = _fxVolume;
        }
    }

    [ContextMenu("Add all SFX source")]
    private void ObtainsAllSFXsources()
    {
        _SFXsources = new List<AudioSource>();
        
        foreach (GameObject source in GameObject.FindGameObjectsWithTag("SFX"))
        {
            _SFXsources.Add(source.GetComponent<AudioSource>());
        }
    }
}
