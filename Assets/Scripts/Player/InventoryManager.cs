using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] GameObject inventoryUI;
    [SerializeField] GameObject[] slots;

    List<String> itemNames = new();

    int currentSelectedSlot = 0;
    int selectedItemIndex = 0;

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

        //SetSelectedSlot(selectedItemIndex);
    }

    void SelectNextSlot()
    {
        if (itemNames.Count == 0) return; // No items to select
        currentSelectedSlot = (currentSelectedSlot + 1) % itemNames.Count;
        SetSelectedSlot(currentSelectedSlot);
    }
    void SelectPreviousSlot()
    {
        if (itemNames.Count == 0) return; // No items to select
        currentSelectedSlot = (currentSelectedSlot - 1 + itemNames.Count) % itemNames.Count;
        SetSelectedSlot(currentSelectedSlot);
    }

    void SetSelectedSlot(int index)
    {
        for (int i = 0; i < itemNames.Count; i++)
        {
            // Activamos el box de selección
            GameObject activeImage = slots[i].transform.Find("Active").gameObject;
            activeImage.SetActive(i == index);
        }
    }

    void CloseInventory()
    {
        /*
        if (itemNames.Count > 0)
        {
            SetSelectedSlot(selectedItemIndex); // Reset to the last selected slot
        }
        */

        if (currentSelectedSlot != selectedItemIndex)
        {
            selectedItemIndex = currentSelectedSlot; // Update the selected item index
            ActiveWeapon activeWeapon = PlayerController.Instance.GetComponent<ActiveWeapon>();
            if (activeWeapon != null)
            {
                string selectedItemName = itemNames[selectedItemIndex];
                activeWeapon.SetActiveWeapon(selectedItemName); // Set the active weapon to the selected item
            }
        }

        InputManager.Instance.ToggleInventory(); // Toggle inventory state
    }

    public void ShowInventory(bool visible)
    {
        inventoryUI.SetActive(visible);
        currentSelectedSlot = selectedItemIndex;
        SetSelectedSlot(selectedItemIndex);
    }
    
    public void AddItem(string itemName, Sprite itemImage)
    {
        if (!itemNames.Contains(itemName))
        {
            itemNames.Add(itemName);
            int index = itemNames.Count - 1;
            if (index < slots.Length)
            {
                GameObject slotImage = slots[index].transform.Find("ItemImage").gameObject;
                slotImage.GetComponent<Image>().sprite = itemImage;
                slotImage.SetActive(true);
                selectedItemIndex = index; // Set the current selected slot to the newly added item
                SetSelectedSlot(index);
            }
            else
            {
                Debug.LogWarning("Not enough slots in the inventory UI.");
            }
        }
    }
}
