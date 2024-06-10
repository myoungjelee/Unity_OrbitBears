using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgmAudioSource;
    public AudioSource gameoverAudioSource;
    public AudioSource shootingAudioSource;
    public AudioSource clickAudioSource;
    public AudioSource mergeAudioSource;

    private static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
            }
            return instance;
        }
    }

    void Start()
    {
        bgmAudioSource.Play();
    }

    public void PlayClickSound()
    {
        clickAudioSource.Play();
    }

    public void PlayGameOverSound()
    {
        gameoverAudioSource.Play();
    }

    public void PlayShootSound()
    {
        shootingAudioSource.Play();
    }

    public void PlayMergeSound()
    {
        mergeAudioSource.Play();
    }

}