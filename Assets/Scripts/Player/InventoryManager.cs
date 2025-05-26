using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] GameObject inventoryUI;
    [SerializeField] GameObject[] slots;

    int currentSelectedSlot = 0;
    int totalItems = 0;

    PlayerControls _controls;


    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        _controls = InputManager.Instance.Controls;
        _controls.Inventory.Close.performed += _ => InputManager.Instance.ToggleInventory();

        totalItems = slots.Length;
        SetSelectedSlot();
    }

    public void ShowInventory(bool visible)
    {
        inventoryUI.SetActive(visible);
    }

    void SetSelectedSlot()
    {
        for (int i = 0; i < totalItems; i++)
        {
            GameObject activeImage = slots[i].transform.Find("Active").gameObject;
            activeImage.SetActive(i == currentSelectedSlot);
        }
    }
}
