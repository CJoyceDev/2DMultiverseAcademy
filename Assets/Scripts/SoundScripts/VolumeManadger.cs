using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeManadger : MonoBehaviour
{
    //Sliders
    [SerializeField] Slider MasterSlider, MusicSlider, SoundSlider;

    //Audio Mixer
    [SerializeField] AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {

        if (!PlayerPrefs.HasKey("masterVolume"))
        {
            PlayerPrefs.SetFloat("masterVolume",1f);
            Load(0);
        }
        else
        {
            Load(0);
        }
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1f);
            Load(1);
        }
        else
        {
            Load(1);
        }
        if (!PlayerPrefs.HasKey("soundVolume"))
        {
            PlayerPrefs.SetFloat("soundVolume", 1f);
            Load(2);
        }
        else
        {
            Load(2);
        }

    }

    public void ChangeMasterVolume(float volume)
    {
        if (audioMixer != null)
        {
            //equation to make sound chnage linear, no clue how it works, but it does //PD
            audioMixer.SetFloat("masterVolume", Mathf.Log10(volume) * 20f);
            Save(0, volume);
        }
    }

    public void ChangeMusicVolume(float volume)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20f);
            Save(1,volume);
        }
    }

    public void ChangeSoundVolume(float volume)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("soundVolume", Mathf.Log10(volume) * 20f);
            Save(2,volume);
        }
    }

    void Load(int x)
    {
        switch (x)
        {
            case 0:
                MasterSlider.value = PlayerPrefs.GetFloat("masterVolume");
                break;
            case 1:
                MusicSlider.value = PlayerPrefs.GetFloat("musicVolume");
                break;
            case 2:
                SoundSlider.value = PlayerPrefs.GetFloat("soundVolume");
                break;
        }
    }

    void Save(int x, float f)
    {
        switch (x)
        {
            case 0:
                PlayerPrefs.SetFloat("masterVolume", f);
                break;
            case 1:
                PlayerPrefs.SetFloat("musicVolume", f);
                break;
            case 2:
                PlayerPrefs.SetFloat("soundVolume", f);
                break;
        }
    }

    
}
