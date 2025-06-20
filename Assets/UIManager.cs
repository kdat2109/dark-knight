using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameplayUI gameplayUI;
    public Shop shop;
    public int gold;
    public GameObject losePanel;
    
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        losePanel.SetActive(false);
    }

    public void ShowLosePanel()
    {
        losePanel.SetActive(true);
    }
    void Update()
    {
        
    }
}
