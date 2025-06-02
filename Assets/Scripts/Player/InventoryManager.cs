using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] GameObject inventoryUI;
    [SerializeField] GameObject[] slots;

    List<String> items = new();

    int currentMarkedSlot = -1; // Currently marked SLOT in the inventory
    int selectedItemIndex = -1; // Currently selected ITEM in the inventory

    PlayerControls _controls;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        _controls = InputManager.Instance.Controls;
        _controls.Inventory.Close.performed += _ => CloseInventory();
        _controls.Inventory.Next.performed += _ => SelectNextSlot();
        _controls.Inventory.Prev.performed += _ => SelectPreviousSlot();
    }

    void SelectNextSlot()
    {
        if (items.Count == 0) return; // No items to select
        currentMarkedSlot = (currentMarkedSlot + 1) % items.Count;
        SetSelectedSlot(currentMarkedSlot);
    }
    void SelectPreviousSlot()
    {
        if (items.Count == 0) return; // No items to select
        currentMarkedSlot = (currentMarkedSlot - 1 + items.Count) % items.Count;
        SetSelectedSlot(currentMarkedSlot);
    }

    void SetSelectedSlot(int index)
    {
        for (int i = 0; i < items.Count; i++)
        {
            // Activamos el box de selección
            GameObject activeImage = slots[i].transform.Find("Active").gameObject;
            activeImage.SetActive(i == index);
        }
    }

    void CloseInventory()
    {
        if (currentMarkedSlot != selectedItemIndex)
        {
            selectedItemIndex = currentMarkedSlot; // Update the selected item index
            ActiveWeapon activeWeapon = PlayerController.Instance.GetComponent<ActiveWeapon>();
            if (activeWeapon != null)
            {
                activeWeapon.SetActiveWeapon(items[selectedItemIndex]); // Set the active weapon to the selected item
            }
        }

        InputManager.Instance.ToggleInventory(); // Toggle inventory state
    }

    public void ShowInventory(bool visible)
    {
        inventoryUI.SetActive(visible);
        if (selectedItemIndex > -1)
        {
            currentMarkedSlot = selectedItemIndex;
            SetSelectedSlot(selectedItemIndex);
        }
    }
    
    public void AddItem(string itemID, Sprite itemImage)
    {
        if (!items.Contains(itemID))
        {
            items.Add(itemID);
            int index = items.Count - 1;
            if (index < slots.Length)
            {
                GameObject slotImage = slots[index].transform.Find("ItemImage").gameObject;
                slotImage.GetComponent<Image>().sprite = itemImage;
                slotImage.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Not enough slots in the inventory UI.");
            }
        }
    }
}
