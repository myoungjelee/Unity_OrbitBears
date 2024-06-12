using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioSource startScenebgmAudioSource;
    public AudioSource bgmAudioSource;
    public AudioSource gameoverAudioSource;
    public AudioSource shootingAudioSource;
    public AudioSource clickAudioSource;
    public AudioSource StartClickAudioSource;
    public AudioSource mergeAudioSource;

    private static SoundManager instance;
    
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBgmSound()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "Start Scene")
        {
            if (!startScenebgmAudioSource.isPlaying)
            {
                startScenebgmAudioSource.Play();
            }
            else
            {
                startScenebgmAudioSource.Stop();
            }
        }
        else
        {
            if (bgmAudioSource != null)
            {
                bgmAudioSource.Play();
            }
        }
    }

    public void PlayClickSound()
    {
        clickAudioSource.Play();
    }

    public void PlayStartClickSound()
    {
        StartClickAudioSource.Play();
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