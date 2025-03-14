using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    //singleton or something, i think i just unlocked a new chromozone, this will be helpful //PD
    public static SoundHandler instance;

    [SerializeField] AudioSource soundObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }



    public void PlaySound(AudioClip sound, Transform soundTransform, float volume)
    {
        Debug.Log("Atempting to play sound");
        //create sound object, assign sound play it and destroy it //PD
        AudioSource audioSource = Instantiate(soundObject, soundTransform.position, Quaternion.identity);
        audioSource.clip = sound;
        audioSource.volume = volume;
        audioSource.Play();

        float soundLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, soundLength);

        //Very clean code i wrote thanks to "Sasquatch B Studious" Yourube channel, big help breaking down how unity sounds work //PD
    }


}
