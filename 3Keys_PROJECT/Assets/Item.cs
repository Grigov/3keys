using System.Diagnostics;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public bool stackable = true;

    [TextArea]
    public string description;

    public virtual void Use()
    {
        //Debug.Log("Использован предмет: " + itemName);
    }

}