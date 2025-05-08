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
        }
        else Destroy(gameObject);
    }

    public bool AddItem(Item newItem)
    {
        bool isFull = true;
        foreach (var slot in slots)
        {
            if (slot.item == null)
            {
                isFull = false;
                break;
            }
        }
        if (isFull)
        {
            UnityEngine.Debug.Log("Инвентарь полон!");
            return false;
        }

        if (newItem.stackable)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item == newItem)
                {
                    slots[i].count++;
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

                if (InventoryUI.Instance != null && InventoryUI.Instance.IsOpen())
                    InventoryUI.Instance.RefreshInventory();

                return true;
            }
        }

        return false;
    }
    public void SwapItems(int indexA, int indexB)
    {
        if (indexA < 0 || indexB < 0 || indexA >= capacity || indexB >= capacity)
        {
            UnityEngine.Debug.LogError($"Invalid indexes: {indexA}, {indexB}");
            return;
        }

        // Меняем местами слоты
        ItemSlot temp = slots[indexA];
        slots[indexA] = slots[indexB];
        slots[indexB] = temp;

        // Обновляем UI
        if (InventoryUI.Instance != null)
            InventoryUI.Instance.RefreshInventory();
    }

}
