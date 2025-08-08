using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameplayUI : MonoBehaviour
{
    [SerializeField] 
    private Stats stats;
    [SerializeField]
    private Image healthBar;
    public TMP_Text goldText;

    public TMP_Text waveText;
    public TMP_Text timeText;
    public TMP_Text healthText;
    
    public TMP_Text userPlayer;

    [SerializeField]
    private MiniHealthBar bossHealthBar;

    public void InitBossHealthBar(Stats stats)
    {
        bossHealthBar.stats = stats;
        bossHealthBar.gameObject.SetActive(true);
    }
    
    public void HideBossHealthBar()
    {
        if(bossHealthBar)
            bossHealthBar.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        SetHealth(stats.data.health,stats.max.health);
    }

    public void SetHealth(float min, float max)
    {
        healthBar.fillAmount = min/ max; // hiệu ứng thanh máu 
        healthText.text = min.ToString("0.0") + "/" + max.ToString("0.0");
    }

    public void SetTime(float time)
    {
        timeText.text = time.ToString("0") + "s";
    }

    public void SetWave(int wave)
    {
        waveText.text = "Wave : " + wave.ToString();
    }

    public void SetGold(int gold)
    {
        goldText.text = gold.ToString();
    }

}
