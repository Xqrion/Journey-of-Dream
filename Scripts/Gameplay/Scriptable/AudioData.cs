using UnityEngine;

namespace MyGameplay.Scriptable
{
    [CreateAssetMenu(fileName = "AudioData", menuName = "Audio/AudioData")]
    [System.Serializable]
    public class AudioData : ScriptableObject
    {
        public AudioClip clip;
        public float volume = 1f;
        public float pitch = 1f;

    }
}