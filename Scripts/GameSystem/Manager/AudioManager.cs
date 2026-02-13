using MyGameplay.Scriptable;
using MyGameSystem.Core;
using UnityEngine;

namespace MyGameSystem.Manager
{
    public class AudioManager : Singleton<AudioManager>
    {
        [Header("===SFX===")]
        [SerializeField] private AudioSource sFXPlayer;

        [Header("===BGM===")]
        [SerializeField] private AudioSource bGMPlayer;
        private GameObject bGMObject;

        protected override void Awake()
        {
            base.Awake();
            bGMObject = bGMPlayer.gameObject;
        }

        public void PlaySFX(AudioClip clip, float volume = 1f, float pitch = 1f)
        {
            sFXPlayer.pitch = pitch;
            sFXPlayer.PlayOneShot(clip, volume);
        }
        public void PlaySFX(AudioData audioData)
        {
            sFXPlayer.pitch = audioData.pitch;
            sFXPlayer.PlayOneShot(audioData.clip, audioData.volume);
        }

        public void PlayBGM(AudioData audioData)
        {
            if (bGMPlayer == null)
            {
                bGMPlayer = bGMObject.AddComponent<AudioSource>();
            }
            sFXPlayer.pitch = audioData.pitch;
            sFXPlayer.PlayOneShot(audioData.clip, audioData.volume);
        }

        public void PlayBGM(AudioClip clip, float volume = 1f, float pitch = 1f)
        {
            if (bGMPlayer == null)
            {
                bGMPlayer = bGMObject.AddComponent<AudioSource>();
            }
            sFXPlayer.pitch = pitch;
            sFXPlayer.PlayOneShot(clip, volume);
        }

        public void StopBGM()
        {
            bGMPlayer.Stop();
            Destroy(bGMPlayer);
        }

    }
}

