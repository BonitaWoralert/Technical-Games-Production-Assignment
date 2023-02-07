using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    // Start is called before the first frame update
    private void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            LoadMusic();
        }
        else
        {
            LoadMusic();
        }

        if (!PlayerPrefs.HasKey("sfxVolume"))
        {
            PlayerPrefs.SetFloat("sfxVolume", 1);
            LoadSfx();
        }
        else
        {
            LoadSfx();
        }
    }

    public void ChangeMusic()
    {
        //AudioListener.volume = musicSlider.value;
        SaveMusic();
    }

    private void LoadMusic()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
    }

    private void SaveMusic()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void ChangeSfx()
    {
        //AudioListener.volume = sfxSlider.value;
        SaveSfx();
    }

    private void LoadSfx()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume");
    }

    private void SaveSfx()
    {
        PlayerPrefs.SetFloat("SfxVolume", sfxSlider.value);
    }
}
