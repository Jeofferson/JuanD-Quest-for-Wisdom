using UnityEngine;
using UnityEngine.Audio;


public class GameAssets : MonoBehaviour
{


    public SoundAudioClip[] soundAudioClipArray;


    private void Awake()
    {

        Instance = this;

    }


    [System.Serializable]
    public class SoundAudioClip
    {

        public SoundManager.Sound sound;
        public AudioClip audioClip;

    }


    public static GameAssets Instance { set; get; }


}
