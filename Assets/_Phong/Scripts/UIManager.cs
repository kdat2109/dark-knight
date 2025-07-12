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
        player.ResetPos();
        waveSystem.ResetWave();
        waveSystem.StartWave();
        GameManager.Instance.IsGameOver = false;
        foreach (var enemy in EnemyManager.Enemies)
        {
            Destroy(enemy.gameObject);
        }
        player.InitPlayer();
        SetGold(10);
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
        LeaderBoard.Instance.AddData(LeaderBoard.Instance.account,waveSystem.currentWave);
        for (int i = 0; i < LeaderBoard.Instance.data.Count; i++)
        {
            if (LeaderBoard.Instance.data[i].account == LeaderBoard.Instance.account)
            {
                Debug.Log(LeaderBoard.Instance.data[i].wave);
                break;
            }
        }
        
        for (int i = 0; i < LeaderBoard.Instance.data.Count; i++)
        {
            Debug.Log(LeaderBoard.Instance.data[i].account+":"+LeaderBoard.Instance.data[i].wave);
        }

        waveSystem.EndWave();
        LeaderBoard.Instance.showLeaderBoardUI();
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