using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour
{
    [SerializeField] private AudioMixer masterMixer;

    public void SetSfxLvl(float sfxLvl)
    {
        masterMixer.SetFloat("SfxVolume", 20f * Mathf.Log10(sfxLvl));
    }

    public void SetMusicLvl(float musicLvl)
    {
        masterMixer.SetFloat("MusicVolume", 20f * Mathf.Log10(musicLvl));
    }
}
