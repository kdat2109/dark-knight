using System;
using System.Collections;
using System.Collections.Generic;
using _Dat;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shop : MonoBehaviour
{
    public List<UpgradeUI> upgrades;
    private Equipment equipment;
    [SerializeField]
    EquipItem[] totalItems;

    [SerializeField]
    private Transform parentItem;
    [SerializeField]
    private ItemUI itemPrefab;
    [SerializeField]
    private Transform parentWeapon;

    public void Register(Equipment equip)
    {
        equipment = equip;
    }
    private void Start()
    {
        ShowShop();
    }

    public void ShowShop()
    {
        gameObject.SetActive(true);
        for (int i = 0; i < upgrades.Count; i++)
        {
            upgrades[i].Init(totalItems[Random.Range(0, totalItems.Length)]);
        }
        
        // show item
        var items = equipment.items;
        for (int i = 0; i < items.Count; i++)
        {
            Instantiate(itemPrefab, parentItem).Init(items[i]);
        }
        
        //show weapon
        var weapon = equipment.GetComponent<WeaponController>().weapons;
        for (int i = 0; i < weapon.Count; i++)
        {
            Instantiate(itemPrefab, parentWeapon).Init(weapon[i]);
        }
    }

}
