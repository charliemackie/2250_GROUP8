using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound {

    // ********
    // Allowing sounds to be managed easier when implementing
    // sound effects
    // ********

    // sound name
    public string name;

    // audio file
    public AudioClip clip;

    // slider for volume and pitch
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;
}
