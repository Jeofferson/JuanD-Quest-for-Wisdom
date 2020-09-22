using UnityEngine;

public static class SoundManager
{


    public enum Sound
    {

        bgm01,
        bgm02,

    }


    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;


    public static void PlaySound(Sound sound)
    {

        if (oneShotGameObject == null)
        {

            oneShotGameObject = new GameObject("One Shot Sound");
            oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();

        }

        oneShotAudioSource.PlayOneShot(GetAudioClip(sound));

        if (sound == Sound.bgm01 || sound == Sound.bgm02)
        {

            oneShotAudioSource.loop = true;

        }

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


    public static void MuteSound()
    {

        oneShotAudioSource.mute = true;

    }


    public static void UnmuteSound()
    {

        oneShotAudioSource.mute = false;

    }


}
