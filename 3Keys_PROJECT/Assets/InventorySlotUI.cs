using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public int slotIndex;
    public InventoryUI inventoryUI;
    CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        if (inventoryUI == null)
            inventoryUI = InventoryUI.Instance;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        if (canvasGroup != null)
            canvasGroup.blocksRaycasts = false;

        eventData.pointerDrag = gameObject;

        UnityEngine.UI.Image iconImage = transform.Find("Icon")?.GetComponent<UnityEngine.UI.Image>();
        if (iconImage != null)
        {
            inventoryUI.BeginDrag(slotIndex, iconImage.sprite);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        if (inventoryUI != null)
        {
            inventoryUI.EndDrag();
        }

        if (canvasGroup != null)
            canvasGroup.blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        inventoryUI.Drag(slotIndex, eventData);
    }

    public void OnDrop(PointerEventData eventData)
    {
        //UnityEngine.Debug.Log($"OnDrop triggered on slot {slotIndex}");

        if (inventoryUI == null)
        {
            //UnityEngine.Debug.LogError("InventoryUI reference is missing!");
            return;
        }

        if (inventoryUI.HasDraggedItem())
        {
            //UnityEngine.Debug.Log($"Dropping item from {inventoryUI.draggedIndex} to {slotIndex}");
            inventoryUI.DropOn(slotIndex);
        }
    }
}
