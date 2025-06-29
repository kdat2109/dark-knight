using System.Collections;
using System.Collections.Generic;
using _Dat;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameplayUI gameplayUI;
    public Shop shop;
    public int gold ;
    public GameObject losePanel;
    public GameObject winPanel;
    public MenuUI menuUI;
    [SerializeField]
    private WaveSystem waveSystem;

    [SerializeField]
    private PlayerController player;
    
    void Awake()
    {
        Instance = this;
        UpdateGoldUI();
        menuUI.gameObject.SetActive(true);
    }

    void Start()
    {
        losePanel.SetActive(false);
    }

    
    public void RestartGame()
    {
        waveSystem.ResetWave();
        waveSystem.StartWave();
        foreach (var enemy in EnemyManager.Enemies)
        {
            Destroy(enemy.gameObject);
        }
        player.InitPlayer();
    }

    public void Home()
    {
        waveSystem.ResetWave();
        menuUI.gameObject.SetActive(true);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
    }
    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
    }
    public void ShowLosePanel()
    {
        losePanel.SetActive(true);
    }


    public void AddGold(int amount)
    {
        gold += amount;
        Debug.Log("Gold : " + gold);
        UpdateGoldUI();
    }

    public void UpdateGoldUI()
    {
        gameplayUI.SetGold(gold);
        shop.SetGold(gold);
    }
    
}