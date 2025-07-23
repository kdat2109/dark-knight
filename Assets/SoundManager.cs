using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;
    private static SoundManager  Instance;

    private void Awake()
    {
        Instance = this;
    }

    public static void PlaySound(AudioClip clip)
    {
        Instance.audioSource.PlayOneShot(clip);
    }
}
