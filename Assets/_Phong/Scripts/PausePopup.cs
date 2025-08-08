using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _Dat;
using TMPro;
using UnityEngine;

public class PausePopup : MonoBehaviour
{
    [SerializeField]
    private WaveSystem waveSystem;
    [SerializeField]
    private TMP_Text info;
    [SerializeField]
    private Stats player;
    private void OnEnable()
    {
        Time.timeScale = 0;
        UpdateInfo();
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    void UpdateInfo()
    {
        var s = new StringBuilder();
        s.AppendLine("<size=65><color=yellow>STATS</color></size>");
        var equipment = player.GetComponent<Equipment>();
        s.AppendLine($"Heath: {player.data.health} (+{equipment.items.Sum(f => f.addStats.health)})");
        s.AppendLine($"Speed: {player.data.speed} (+{equipment.items.Sum(f => f.addStats.speed)})");
        s.AppendLine($"Attack Speed: {player.data.attackSpeed} (+{equipment.items.Sum(f => f.addStats.attackSpeed)})");
        s.AppendLine($"Damage: {player.data.damage} (+{equipment.items.Sum(f => f.addStats.damage)})");
        info.SetText(s);
    }

    public void Home()
    {
        waveSystem.EndWave();
        UIManager.Instance.Home();
        gameObject.SetActive(false);
    }
    
    
}
