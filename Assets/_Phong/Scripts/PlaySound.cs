using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField]
    private AudioClip audio;
    void Start()
    {
        SoundManager.PlaySound(audio);
    }
}
