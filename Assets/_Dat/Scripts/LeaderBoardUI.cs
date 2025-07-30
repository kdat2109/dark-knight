using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderBoardUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text accountText,waveText;

    public void SetData(string account, int wave)
    {
        accountText.text = account;
        waveText.text = "Wave : " + wave;
    }

}
