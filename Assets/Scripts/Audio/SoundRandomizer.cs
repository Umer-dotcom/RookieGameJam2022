using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
 public class SoundRandomizer : MonoBehaviour
{
    [SerializeField] List<AudioClip> audioClips;
    [Range(0f, 0.5f)]
    [SerializeField] float volumeChangeMultiplier;
    [Range(0f, 0.5f)]
    [SerializeField] float pitchChangeMultiplier;
    [Range(0.75f, 1f)]
    [SerializeField] float maxVolume;
    [SerializeField] bool playSound;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayRandomClip()
    {
        if (playSound) { 
        audioSource.clip = audioClips[Random.Range(0, audioClips.Count)];
        audioSource.volume = Random.Range(maxVolume - volumeChangeMultiplier, maxVolume);
        audioSource.pitch = Random.Range(1 - pitchChangeMultiplier, 1 + pitchChangeMultiplier);
        audioSource.PlayOneShot(audioSource.clip);
        }
    }
    public void PlayRandomClipAtVolume(float volume)
    {
        if (playSound)
        {
            //audioSource.clip = audioClips[Random.Range(0, audioClips.Count)];
            audioSource.volume = volume;
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}
