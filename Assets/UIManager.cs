using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameplayUI gameplayUI;
    public Shop shop;
    public int gold ;
    public GameObject losePanel;
    
    void Awake()
    {
        Instance = this;
        UpdateGoldUI();
    }

    void Start()
    {
        losePanel.SetActive(false);
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
