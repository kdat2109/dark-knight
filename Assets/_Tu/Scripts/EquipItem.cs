using UnityEngine;

public enum EquipType
{
    Weapon,
    Item,
}
public class EquipItem :  MonoBehaviour
{
    public SpriteRenderer image;

    public int gold;
    public string itemName;
    public EquipType type;
    public Stats.Data addStats;
}