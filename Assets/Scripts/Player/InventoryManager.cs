using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] GameObject inventoryUI;
    [SerializeField] GameObject[] slots;

    int currentSelectedSlot = 0;
    int selectedItemIndex = 0;
    int totalItems = 0;

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

        totalItems = slots.Length;
        SetSelectedSlot(selectedItemIndex);
    }

    void SelectNextSlot()
    {
        currentSelectedSlot = (currentSelectedSlot + 1) % totalItems;
        SetSelectedSlot(currentSelectedSlot);
    }
    void SelectPreviousSlot()
    {
        currentSelectedSlot = (currentSelectedSlot - 1 + totalItems) % totalItems;
        SetSelectedSlot(currentSelectedSlot);
    }

    public void ShowInventory(bool visible)
    {
        inventoryUI.SetActive(visible);
    }

    void SetSelectedSlot(int index)
    {
        for (int i = 0; i < totalItems; i++)
        {
            GameObject activeImage = slots[i].transform.Find("Active").gameObject;
            activeImage.SetActive(i == index);
        }
    }

    void CloseInventory()
    {
        SetSelectedSlot(selectedItemIndex); // Reset to the last selected slot
        InputManager.Instance.ToggleInventory(); // Toggle inventory state
    }
}
