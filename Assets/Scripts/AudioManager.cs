using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();

            sound.audioSource.clip = sound.audioClip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.loop = sound.loop;
        }
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, soundToFind => soundToFind.name == name);

        if (sound == null)
        {
            Debug.LogWarning("Sound \"" + name + "\" not found!");
            return;
        }

        sound.audioSource.Play();
    }

    public void Mute(String name)
    {
        Sound sound = Array.Find(sounds, soundToFind => soundToFind.name == name);

        if (sound == null)
        {
            Debug.LogWarning("Sound \"" + name + "\" not found!");
            return;
        }

        sound.audioSource.mute = true;
    }

    public void Unmute(String name)
    {
        Sound sound = Array.Find(sounds, soundToFind => soundToFind.name == name);

        if (sound == null)
        {
            Debug.LogWarning("Sound \"" + name + "\" not found!");
            return;
        }

        sound.audioSource.mute = false;
    }

    public void adjustVolume(string name, float newVolume)
    {
        Sound sound = Array.Find(sounds, soundToFind => soundToFind.name == name);

        if (sound == null)
        {
            Debug.LogWarning("Sound \"" + name + "\" not found!");
            return;
        }

        sound.audioSource.volume = newVolume;
    }

    public void Pause(string name)
    {
        Sound sound = Array.Find(sounds, soundToFind => soundToFind.name == name);

        if (sound == null)
        {
            Debug.LogWarning("Sound \"" + name + "\" not found!");
            return;
        }

        sound.audioSource.Pause();
    }

    public void Stop(string name)
    {
        Sound sound = Array.Find(sounds, soundToFind => soundToFind.name == name);

        if (sound == null)
        {
            Debug.LogWarning("Sound \"" + name + "\" not found!");
            return;
        }

        sound.audioSource.Stop();
    }

}