using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField]
    private DamagePopup damagePopup;
    public bool IsGameOver = false;
    public bool IsGamePaused = false;
    public DataPlayer Profile { get; set; }

    public PlayableDirector playableDirector;

    public void PlayIntro(Action complete)
    {
        StartCoroutine(StartIntro(complete));
    }

    IEnumerator StartIntro(Action complete)
    {
        playableDirector.gameObject.SetActive(true);
        yield return new WaitUntil(() => playableDirector.time >= playableDirector.duration-0.25f);
        playableDirector.gameObject.SetActive(false);
        complete();
    }

    public void SaveData(int wave,List<string> items)
    {
        Profile.currentWave = wave;
        if (wave == 0)
        {
            Profile.gold = 10;
        }
        else
        {
            Profile.gold = UIManager.Instance.gold;
        }
        
        Profile.dataEquip = new List<string>(items);

        FirebaseManager.Instance.SetData();
    }

    public void SetMaxWaveData(int wave)
    {
        if(Profile.maxWave <  wave)
        {
            Profile.maxWave = wave;
        }
    }

    public void ShowDamagePopup(string text, Color color,Vector3 position)
    {
        Instantiate(damagePopup, position, Quaternion.identity).Show(text, color);
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
}
