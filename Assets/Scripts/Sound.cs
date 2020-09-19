using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class Sound
{


    public string name;

    public AudioClip audioClip;

    [HideInInspector]
    public AudioSource audioSource;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;


}
