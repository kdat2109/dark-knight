using System.Collections;
using System.Collections.Generic;
using _Dat;
using TMPro;
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
        var data = GameManager.Instance.Profile;
        player.ResetPos();
        waveSystem.SetWave(data.currentWave);
        waveSystem.StartWave();
        GameManager.Instance.IsGameOver = false;
        foreach (var enemy in EnemyManager.Enemies)
        {
            Destroy(enemy.gameObject);
        }
        player.InitPlayer();
        SetGold(data.gold);
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
        waveSystem.EndWave();
        winPanel.SetActive(true);
    }
    public void ShowLosePanel()
    {
        GameManager.Instance.SetMaxWaveData(waveSystem.currentWave);
        GameManager.Instance.SaveData(0,new List<string>());
        LeaderBoard.Instance.AddData(GameManager.Instance.Profile.name,waveSystem.currentWave);
        waveSystem.EndWave();
        losePanel.SetActive(true);
    }




    public void AddGold(int amount)
    {
        gold += amount;
        Debug.Log("Gold : " + gold);
        UpdateGoldUI();
    }

    public void SetGold(int amount)
    {
        gold = amount;
        Debug.Log("Gold : " + gold);
        UpdateGoldUI();
    }

    public void UpdateGoldUI()
    {
        gameplayUI.SetGold(gold);
        shop.SetGold(gold);
    }
    
}