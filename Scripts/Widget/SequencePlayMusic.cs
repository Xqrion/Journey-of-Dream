using UnityEngine;

public class SequencePlayMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] sounds;
    public float minDelay;
    public float maxDelay;
    AudioClip currentSound;

    float currentTime;
    float playTime;
    public int index;

    private void Start()
    {
        audioSource.PlayOneShot(sounds[index]);
    }

    private void SetupSound()
    {
        index++;
        index %= sounds.Length;
        currentSound = sounds[index];
        playTime = Random.Range(minDelay, maxDelay) + currentSound.length;
        currentTime = 0;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > playTime)
        {
            SetupSound();
            audioSource.PlayOneShot(currentSound);
        }
    }

}
