using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource snakeAudioSource;
    [SerializeField] AudioClip arrowSound;
    [SerializeField] AudioClip textSound;
    [SerializeField] AudioClip operateSound;
    [SerializeField] AudioClip explodeSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip snakeSound;

    private static SoundManager instance;
    private static SoundManager Instance
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

    public static void PlayArrowSound()
    {
        Instance.audioSource.clip = Instance.arrowSound;
        Instance.audioSource.Play();
    }
    
    public static void PlayTextSound()
    {
        Instance.audioSource.clip = Instance.textSound;
        Instance.audioSource.Play();
    }

    public static void PlayOperateSound()
    {
        Instance.audioSource.clip = Instance.operateSound;
        Instance.audioSource.Play();
    }

    public static void PlayExplodeSound()
    {
        Instance.audioSource.clip = Instance.explodeSound;
        Instance.audioSource.Play();
    }

    public static void PlayDeathSound()
    {
        Instance.audioSource.clip = Instance.deathSound;
        Instance.audioSource.Play();
    }

    public static void PlaySnakeSound()
    {
        Instance.snakeAudioSource.clip = Instance.snakeSound;
        Instance.snakeAudioSource.Play();
    }
}
