using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField]
    private Image  image;

    public void Init(EquipItem equipItem)
    {
        image.sprite = equipItem.image.sprite;
    }
}
