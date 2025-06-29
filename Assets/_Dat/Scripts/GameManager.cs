using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField]
    private DamagePopup damagePopup;
    public bool IsGameOver = false;
    public bool IsGamePaused = false;

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
