using System.Collections;
using System.Collections.Generic;
using _Dat;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{

    [SerializeField] private TMP_Text goldText, infoText, itemNameText;

    [SerializeField]
    private Image image;
    
    [SerializeField]
    private Button buyButton;

    private EquipItem currentEquipItem;   
    private Shop shop;
    
    
    
    public void Init(EquipItem equipItem, Shop shop)
    {
        this.shop = shop;
        currentEquipItem = equipItem;
        
        image.sprite = equipItem.image.sprite;
        goldText.text = "-" + equipItem.gold;
        itemNameText.text = equipItem.itemName;
        
      
        
        
        
        string info = "";
        if (equipItem.addStats.health != 0)
        {
            info += equipItem.addStats.health.ToString("0.0")+ " health";
        }

        if (equipItem.addStats.damage != 0)
        {
            info += "\n"+equipItem.addStats.damage.ToString("0.0") + " damage";
        }

        if (equipItem.addStats.speed != 0)
        {
            info += "\n"+equipItem.addStats.speed.ToString("0.0")+ " speed";
        }

        if (equipItem.addStats.attackSpeed != 0)
        {
            info += "\n"+equipItem.addStats.attackSpeed.ToString("0.0")+ " attack speed";
            
        }
        
        
        Weapon weapon = equipItem.GetComponent<Weapon>();
        if (weapon != null)
        {
           info+= weapon.damage.ToString("0.0") + " damage";
        }
        if (weapon != null)
        {
            info+= "\n" + weapon.range.ToString("0.0") + " range";
        }
        if (weapon != null)
        {
            info+= "\n" + weapon.fireRate.ToString("0.0") + " fireRate";
        }
        
        Bow bow = equipItem.GetComponent<Bow>();
        if (bow != null)
        {
            info += "\n" + bow.bulletSpeed.ToString("0.0") + " bulletSpeed";
        }
        infoText.text = info;
        
        
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(Buy);
        gameObject.SetActive(true);

        UpdateButtonBuy();
    }

    public void UpdateButtonBuy()
    {
        var canBuy = UIManager.Instance.gold >= currentEquipItem.gold;
        if (canBuy)
        {
            buyButton.interactable = true;
        }
        else
        {
            buyButton.interactable = false;
        }
    }
    
    private void Buy()
    {
        shop.Equip(currentEquipItem);
        Debug.Log($"equip item {currentEquipItem.name}");
        gameObject.SetActive(false);
    }
    
  
    
}
