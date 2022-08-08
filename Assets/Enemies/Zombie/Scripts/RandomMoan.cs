using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMoan : MonoBehaviour
{
    public List<AudioClip> audioClips;
    public AudioSource audioSource;

    void Start()
    {
        StartCoroutine(PlayRandomAudio());
    }

    IEnumerator PlayRandomAudio()
    {
        for(; ; )
        {
            audioSource.clip = audioClips[Random.Range(0, audioClips.Count)];
            audioSource.Play();

            yield return new WaitForSeconds(Random.Range(4, 10));
        }
    }
}
