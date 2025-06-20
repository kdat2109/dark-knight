using System;
using System.Collections;
using System.Collections.Generic;
using _Dat;
using UnityEngine;
using Random = UnityEngine.Random;

public class Equipment : MonoBehaviour
{
    public List<EquipItem> items;
    [SerializeField]
    private Stats stats;
    [SerializeField]
    private WeaponController weaponController;
    [SerializeField]
    private EquipItem[] testEquip;
    private void Start()
    {
        //start game thì đăng kí người chơi vơi shop
        UIManager.Instance.shop.Register(this);
        foreach (var equipItem in testEquip)
        {
            Equip(equipItem);
        }
    }

    public void Equip(EquipItem equipItem)
    {
        switch (equipItem.type)
        {
            case EquipType.Weapon:
                EquipWeapons(equipItem);  
                break;
            case EquipType.Item:
                EquipItems(equipItem); 
                break;
        }
    }

    void EquipWeapons(EquipItem item)
    {
        Debug.Log($"equip weapon {item.name}");
        Weapon weapon = item.GetComponent<Weapon>();
        weaponController.Equip(weapon);
    }

    void EquipItems(EquipItem item)
    {
        Debug.Log($"equip item {item.name}");
        items.Add(item);
        stats.AddStats(item.addStats);
    }
    
    public void UnEquip()
    {
        
    }

}


