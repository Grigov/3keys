using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public class ItemSlot
    {
        public Item item;
        public int count;

        public ItemSlot(Item item = null, int count = 0)
        {
            this.item = item;
            this.count = count;
        }
    }

    public static Inventory Instance;
    public ItemSlot[] slots;
    public int capacity = 9;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            slots = new ItemSlot[capacity];
            for (int i = 0; i < capacity; i++)
            {
                slots[i] = new ItemSlot();
            }
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void RemoveItem(int index, int amount = 1)
    {
        if (index < 0 || index >= slots.Length) return;

        var slot = slots[index];
        if (slot.item == null || slot.count <= 0) return;

        slot.count -= amount;

        if (slot.count <= 0)
            slot.item = null;

        InventoryUI.Instance?.RefreshInventory();
    }

    public bool AddItem(Item newItem)
    {
        if (newItem.stackable)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null && slots[i].item == newItem)
                {
                    slots[i].count++;
                    InventoryUI.Instance?.RefreshInventory();
                    return true;
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].item = newItem;
                slots[i].count = 1;

                InventoryUI.Instance?.RefreshInventory();
                return true;
            }
        }

        UnityEngine.Debug.Log("Инвентарь полон!");
        return false;
    }

    public void SwapItems(int indexA, int indexB)
    {
        if (indexA < 0 || indexB < 0 || indexA >= capacity || indexB >= capacity)
        {
            UnityEngine.Debug.LogError($"Invalid indexes: {indexA}, {indexB}");
            return;
        }

        ItemSlot temp = slots[indexA];
        slots[indexA] = slots[indexB];
        slots[indexB] = temp;

        if (InventoryUI.Instance != null)
            InventoryUI.Instance.RefreshInventory();
    }

}
