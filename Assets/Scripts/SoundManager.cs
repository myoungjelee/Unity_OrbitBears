using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
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
        BGMPlaySound();
    }

    public void BGMPlaySound()
    {
        // 오디오 클립 설정 및 재생
        audioSource.clip = bgmClip;
        audioSource.Play();
            }

    public void ClickPlaySound()
    {
        audioSource.clip = clickClip;
        audioSource.Play();
    }

    public void GameOverPlaySound()
    {
        audioSource.clip = gameoverClip;
        audioSource.Play();

    }

    public void ShootingPlaySound()
    {
        audioSource.clip = shootingClip;
        audioSource.Play();
    }

    public void AddPlaySound()
    {
        audioSource.clip = addClip;
        audioSource.Play();
    }
}