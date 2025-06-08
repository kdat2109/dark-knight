using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniHealthBar : MonoBehaviour
{
    [SerializeField]
    private Image fill;
    [SerializeField]
    private Stats stats;

    private void Update()
    {
        fill.fillAmount = stats.data.health/stats.max.health;
    }
}
