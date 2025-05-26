using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private GameObject inventoryUI;

    PlayerControls _controls;

    protected override void Awake()
    {
        base.Awake();

        _controls = InputManager.Instance.Controls;
        _controls.Inventory.Close.performed += _ => InputManager.Instance.ToggleInventory();
    }
}
