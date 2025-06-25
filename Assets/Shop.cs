using System;
using System.Collections;
using System.Collections.Generic;
using _Dat;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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

    [SerializeField] 
    private Button rollButton;

    [SerializeField] 
    private Button goWave;
    [SerializeField]
    private WaveSystem waveSystem;
    [SerializeField]
    private TMP_Text goldText;
    UIManager goldUIManager;
    
    
    private ItemUI itemUI;
    public void Register(Equipment equip)
    {
        equipment = equip;
    }
    private void Start()
    {
        ShowShop();
        RollItems();
        
        rollButton.onClick.AddListener(RollItems);
        goWave.onClick.AddListener(NextWave);
    }

    public void Equip(EquipItem item)
    {
        UIManager.Instance.AddGold(-item.gold);
        equipment.Equip(item);
        ShowShop();
        UpdateBuyButton();

    }

    void UpdateBuyButton()
    {
        for (int i = 0; i < upgrades.Count; i++)
        {
            upgrades[i].UpdateButtonBuy();
        }
    }
    public void ShowShop()
    {
        gameObject.SetActive(true);

        for (int i = parentItem.childCount - 1; i >= 0; i--)
        {
            Destroy(parentItem.GetChild(i).gameObject);
        }
        
        for (int i = parentWeapon.childCount - 1; i >= 0; i--)
        {
            Destroy(parentWeapon.GetChild(i).gameObject);
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

    public void RollItems()
    {
        for (int i = 0; i < upgrades.Count; i++)
        {
            upgrades[i].Init(totalItems[Random.Range(0, totalItems.Length)],this);
        }
    }

    void NextWave()
    {
        waveSystem.NextWave();
        gameObject.SetActive(false);
    }

    public void SetGold(int gold)
    {
        goldText.text = gold.ToString();
    }
    
}
