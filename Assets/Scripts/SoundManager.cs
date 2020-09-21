﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{


    public enum Sound
    {
        bgm01,
        bgm02,
    }


    public static void PlaySound(Sound sound)
    {

        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));

    }


    private static AudioClip GetAudioClip(Sound sound)
    {

        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.Instance.soundAudioClipArray)
        {

            if (soundAudioClip.sound == sound)
            {

                return soundAudioClip.audioClip;

            }

        }

        Debug.LogError("Sound " + sound + " not found!");
        return null;

    }


}
