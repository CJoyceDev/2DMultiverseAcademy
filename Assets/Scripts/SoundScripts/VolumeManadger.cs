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
        
    }

    public void ChangeMasterVolume(float volume)
    {
        if (MasterSlider != null)
        {
            AudioListener.volume = MasterSlider.value;
        }
    }

    public void ChangeMusicVolume(float volume)
    {
        if (MusicSlider != null)
        {
            AudioListener.volume = MasterSlider.value;
        }
    }

    public void ChangeSoundVolume(float volume)
    {
        if (SoundSlider != null)
        {
            AudioListener.volume = MasterSlider.value;
        }
    }


}
