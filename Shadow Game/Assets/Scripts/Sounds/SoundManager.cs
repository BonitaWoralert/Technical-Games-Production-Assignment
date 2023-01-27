using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    // Start is called before the first frame update
    void Start()
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
        AudioListener.volume = musicSlider.value;
        SaveMusic();
    }

    private void LoadMusic()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void SaveMusic()
    {
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }

    public void ChangeSfx()
    {
        AudioListener.volume = sfxSlider.value;
        SaveSfx();
    }

    private void LoadSfx()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }

    private void SaveSfx()
    {
        PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
    }
}
