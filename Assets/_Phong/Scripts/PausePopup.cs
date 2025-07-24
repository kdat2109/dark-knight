using System;
using System.Collections;
using System.Collections.Generic;
using _Dat;
using UnityEngine;

public class PausePopup : MonoBehaviour
{
    [SerializeField]
    private WaveSystem waveSystem;
    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void Home()
    {
        waveSystem.EndWave();
        UIManager.Instance.Home();
        gameObject.SetActive(false);
    }
    
}
