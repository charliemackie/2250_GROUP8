using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    // create singleton
    public static AudioManager instance;

    // Create array of sounds that will be managed by the developer
    public Sound[] sounds;

    void Awake()
    {
        instance = this;

        // For each sound, create an audio source component
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    // Play the audio from the Audio Manager singleton
    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
