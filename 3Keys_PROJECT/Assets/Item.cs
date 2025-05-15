using System.Diagnostics;
using UnityEngine;

public enum ItemType
{
    Defalt,
    Weapon,
    Consumable
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Header("Shop")]
    public int sellPrice = 10;

    public string itemName;
    public Sprite icon;
    public bool stackable = true;
    public int restoreAmount;
    public ItemType itemType;

    [TextArea]
    public string description;

    public virtual void Use()
    {
    }

}