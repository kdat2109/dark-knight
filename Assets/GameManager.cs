using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField]
    private DamagePopup damagePopup;

    public void ShowDamagePopup(string text, Color color,Vector3 position)
    {
        Instantiate(damagePopup, position, Quaternion.identity).Show(text, color);
    }

    void Awake()
    {
        Instance = this;
    }
    
}
