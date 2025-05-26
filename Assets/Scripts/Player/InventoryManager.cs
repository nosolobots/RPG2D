using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private GameObject inventoryUI;

    public bool IsInventoryOpen { get; private set; } = false;

    protected override void Awake()
    {
        base.Awake();
    }


    public void ToggleInventory(PlayerControls controls)
    {
        IsInventoryOpen = !IsInventoryOpen;

        if (IsInventoryOpen)
        {
            // Open the inventory UI
            controls.Player.Disable(); // Disable player controls when inventory is open
            controls.Inventory.Enable(); // Enable inventory controls if needed
            Time.timeScale = 0; // Uncomment if you want to pause the game
            inventoryUI.SetActive(true);
        }
        else
        {
            // Close the inventory UI
            controls.Player.Enable(); // Re-enable player controls when inventory is closed
            controls.Inventory.Disable(); // Disable inventory controls
            Time.timeScale = 1; // Uncomment if you want to resume the game
            inventoryUI.SetActive(false);
        }
    }
}
