using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAudio : MonoBehaviour
{
    private AudioSource Audio;
    public AudioClip MapSound;


    void Start()
    {
        Audio = gameObject.AddComponent<AudioSource>(); // Add AudioSource component
        Audio.clip = MapSound;
        Audio.volume = 0.15f;
        Audio.Play();

        StartCoroutine(RepeatAudio());
    }

    IEnumerator RepeatAudio()
    {
        while (true)
        {
            yield return new WaitForSeconds(Audio.clip.length);
            Audio.Play();
        }
    }
}
