using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using UnityEngine.EventSystems;
using System.Diagnostics;


public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public Transform slotContainer;
    public GameObject descriptionPanel;
    public UnityEngine.UI.Text descriptionText;
    public static InventoryUI Instance;
    public Canvas canvas;
    public GameObject dragIconObject;
    public UnityEngine.UI.Image dragIconImage;
    public int draggedIndex = -1;
    CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsOpen()
    {
        return inventoryPanel.activeSelf;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            bool isActive = inventoryPanel.activeSelf;
            inventoryPanel.SetActive(!isActive);

            if (!isActive) RefreshInventory();
        }
    }

    void OnItemClick(int index)
    {
        UnityEngine.Debug.Log($"OnItemClick! i = {index}");
        var slot = Inventory.Instance.slots[index];
        if (slot.item == null) return;
        Item item = slot.item;

        switch (item.itemType)
        {
            case ItemType.Weapon:
                PlayerEquipment.Instance.EquipWeapon(item as WeaponItem);
                break;

            case ItemType.Consumable:
                UseConsumable(item, index);
                break;
        }

        if (index < 0 || index >= Inventory.Instance.slots.Length)
        {
            UnityEngine.Debug.LogError($"Некорректный индекс: {index}");
            return;
        }

        Inventory.ItemSlot itemSlot = Inventory.Instance.slots[index];

        if (itemSlot.item == null)
        {
            UnityEngine.Debug.Log("Слот пуст!");
            return;
        }

        if (itemSlot.item is WeaponItem weaponItem)
        {
            if (PlayerEquipment.Instance != null)
            {
                PlayerEquipment.Instance.EquipWeapon(weaponItem);
            }
            else
            {
                UnityEngine.Debug.LogError("Экземпляр PlayerEquipment не найден!");
            }
        }
    }

    void UseConsumable(Item item, int slotIndex)
    {
        UnityEngine.Debug.Log($"UseConsumable! item = {item}, slot i = {slotIndex}");
        Health playerHealth = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Health>();
        if (playerHealth == null) return;

        playerHealth.Heal(item.restoreAmount);

        Inventory.Instance.slots[slotIndex].count--;

        if (Inventory.Instance.slots[slotIndex].count <= 0)
            Inventory.Instance.slots[slotIndex].item = null;

        InventoryUI.Instance?.RefreshInventory();
    }

    public void RefreshInventory()
    {
        foreach (Transform child in slotContainer)
            Destroy(child.gameObject);

        for (int i = 0; i < Inventory.Instance.slots.Length; i++)
        {
            GameObject slot = Instantiate(slotPrefab, slotContainer);
            slot.GetComponent<RectTransform>().localScale = Vector3.one;

            var slotUI = slot.GetComponent<InventorySlotUI>();
            slotUI.slotIndex = i;
            slotUI.inventoryUI = this;

            UnityEngine.UI.Image iconImage = slot.transform.Find("Icon").GetComponent<UnityEngine.UI.Image>();
            UnityEngine.UI.Text countText = slot.transform.Find("Icon/CountText").GetComponent<UnityEngine.UI.Text>();

            if (i < Inventory.Instance.slots.Length && Inventory.Instance.slots[i].item != null)
            {
                Inventory.ItemSlot itemSlot = Inventory.Instance.slots[i];
                iconImage.sprite = itemSlot.item.icon;
                iconImage.enabled = true;
                countText.text = itemSlot.count > 1 ? "x" + itemSlot.count : "";
            }
            else
            {
                iconImage.sprite = null;
                iconImage.enabled = false;
                countText.text = "";
            }

            Button button = slot.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            int currentIndex = i;
            button.onClick.AddListener(() => OnItemClick(currentIndex));

            EventTrigger trigger = slot.GetComponent<EventTrigger>();
            if (trigger == null) trigger = slot.AddComponent<EventTrigger>();

            EventTrigger.Entry rightClickEntry = new EventTrigger.Entry();
            rightClickEntry.eventID = EventTriggerType.PointerClick;
            rightClickEntry.callback.AddListener((data) =>
            {
                PointerEventData ped = (PointerEventData)data;
                if (ped.button == PointerEventData.InputButton.Right)
                {
                    ShowItemDescription(currentIndex, ped.position);
                }
            });

            trigger.triggers.Add(rightClickEntry);
        }
    }

    public void HideDescription()
    {
        descriptionPanel.gameObject.SetActive(false);
    }

    public void ShowItemDescription(int index, Vector2 screenPosition)
    {
        
        var slots = Inventory.Instance.slots;
        if (index < 0 || index >= Inventory.Instance.slots.Length) return;
        
        Inventory.ItemSlot slot = Inventory.Instance.slots[index];

        if (slot.item == null)
        {
            //UnityEngine.Debug.Log("Слот пуст!");
            return;
        }

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            screenPosition,
            canvas.worldCamera,
            out Vector2 localPoint
        );

        descriptionPanel.SetActive(true);
        descriptionText.text = slot.item.itemName + "\n\n" + slot.item.description;
        localPoint += new Vector2(45, 45);
        descriptionPanel.GetComponent<RectTransform>().anchoredPosition = localPoint;
    }

    public void BeginDrag(int index, Sprite sprite)
    {
        draggedIndex = index;
        dragIconImage.sprite = sprite;
        dragIconImage.enabled = true;
        dragIconObject.SetActive(true);
    }

    public void Drag(int index, PointerEventData data)
    {
        Inventory.ItemSlot slot = Inventory.Instance.slots[index];
        if (slot.item == null)
        {
            EndDrag();
            return;
        }
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            dragIconObject.transform.parent as RectTransform,
            data.position,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out Vector2 localPoint
        );

        dragIconObject.GetComponent<RectTransform>().anchoredPosition = localPoint;
    }

    public void EndDrag()
    {
        dragIconObject.SetActive(false);
        draggedIndex = -1;

        RefreshInventory();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        if (canvasGroup != null)
            canvasGroup.blocksRaycasts = true;

        EndDrag();
    }

    public void DropOn(int targetIndex)
    {
        if (draggedIndex == -1 || draggedIndex == targetIndex) return;

        if (draggedIndex < 0 || targetIndex < 0 ||
            draggedIndex >= Inventory.Instance.capacity ||
            targetIndex >= Inventory.Instance.capacity)
        {
            UnityEngine.Debug.LogError($"Некорректные индексы: dragged={draggedIndex}, target={targetIndex}");
            return;
        }

        Inventory.Instance.SwapItems(draggedIndex, targetIndex);
        RefreshInventory();
        EndDrag();
    }

    public bool HasDraggedItem()
    {
        return draggedIndex >= 0;
    }
}
