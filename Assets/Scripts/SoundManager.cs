using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource { get; private set; }
    public AudioClip bgmClip;
    public AudioClip gameoverClip;
    public AudioClip shootingClip;
    public AudioClip clickClip;
    public AudioClip addClip;

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
        // AudioSource 컴포넌트 추가
        audioSource = gameObject.GetComponent<AudioSource>();
        PlayBgmSound();
    }

    public void PlayBgmSound()
    {
        // 오디오 클립 설정 및 재생
        audioSource.clip = bgmClip;
        audioSource.Play();
    }

    public void PlayClickSound()
    {
        audioSource.clip = clickClip;
        audioSource.Play();
    }

    public void PlayGameOverSound()
    {
        audioSource.clip = gameoverClip;
        audioSource.Play();

    }

    public void PlayShootSound()
    {
        audioSource.clip = shootingClip;
        audioSource.Play();
    }

    public void PlayMergeSound()
    {
        audioSource.clip = addClip;
        audioSource.Play();
    }

}