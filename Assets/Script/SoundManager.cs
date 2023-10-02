using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set;}
    private AudioSource Source;

    private void Awake()
    {
        instance = this;
        Source = GetComponent<AudioSource>();
    }
    public void playSound(AudioClip _sound)
    {
        Source.PlayOneShot(_sound);
    }
   
}
