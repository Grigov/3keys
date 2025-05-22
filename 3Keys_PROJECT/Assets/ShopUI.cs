using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class ShopUI : MonoBehaviour
{
    public GameObject sellSlotPrefab;
    public Transform sellSlotContainer;
    public static ShopUI Instance;
    public List<Item> itemsForSale;
    public GameObject buySlotPrefab;
    public Transform buySlotContainer;
    public Transform weaponSlot;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowSellMenu()
    {
        foreach (Transform child in sellSlotContainer)
            Destroy(child.gameObject);

        var slots = Inventory.Instance.slots;

        for (int i = 0; i < slots.Length; i++)
        {
            var slot = slots[i];
            if (slot.item == null) continue;

            GameObject uiSlot = Instantiate(sellSlotPrefab, sellSlotContainer);

            uiSlot.transform.Find("Icon").GetComponent<UnityEngine.UI.Image>().sprite = slot.item.icon;
            uiSlot.transform.Find("Name").GetComponent<UnityEngine.UI.Text>().text = slot.item.itemName;
            uiSlot.transform.Find("Price").GetComponent<UnityEngine.UI.Text>().text = $"Цена: {slot.item.sellPrice}ʓ";
            uiSlot.transform.Find("Count").GetComponent<UnityEngine.UI.Text>().text = "x" + slot.count;

            int indexCopy = i;
            Button slotButton = uiSlot.GetComponentInChildren<Button>();
            if (slotButton != null)
            {
                slotButton.onClick.AddListener(() =>
                {
                    SellItem(indexCopy);

                    if (weaponSlot != null)
                    {
                        foreach (Transform child in weaponSlot)
                        {
                            if (child != null)
                                Destroy(child.gameObject);
                        }
                    }
                });
            }
        }
    }

    public void ShowBuyMenu()
    {
        foreach (Transform child in buySlotContainer)
            Destroy(child.gameObject);

        foreach (Item item in itemsForSale)
        {
            GameObject slot = Instantiate(buySlotPrefab, buySlotContainer);

            slot.transform.Find("Icon").GetComponent<UnityEngine.UI.Image>().sprite = item.icon;
            slot.transform.Find("Name").GetComponent<UnityEngine.UI.Text>().text = item.itemName;
            slot.transform.Find("Price").GetComponent<UnityEngine.UI.Text>().text = $"Цена: {item.sellPrice}ʓ";

            Item itemCopy = item;
            slot.GetComponentInChildren<Button>()?.onClick.AddListener(() => BuyItem(item));
        }
    }

    public void SellItem(int index)
    {
        var slot = Inventory.Instance.slots[index];
        if (slot.item == null || slot.count <= 0) return;

        PlayerWallet.Instance.AddGold(slot.item.sellPrice);

        slot.count--;

        if (slot.count <= 0)
            slot.item = null;

        ShowSellMenu();
        InventoryUI.Instance?.RefreshInventory();
    }

    public void BuyItem(Item item)
    {
        if (DataPlayer.money < item.sellPrice)
        {
            UnityEngine.Debug.Log("Не хватает золота!");
            return;
        }

        bool added = Inventory.Instance.AddItem(item);
        if (!added)
        {
            UnityEngine.Debug.Log("Нет места в инвентаре!");
            return;
        }

        DataPlayer.money -= item.sellPrice;
        UnityEngine.Debug.Log($"Куплен предмет: {item.itemName}");
        InventoryUI.Instance?.RefreshInventory();
    }

}
