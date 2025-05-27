using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    PlayerControls _controls;

    public PlayerControls Controls => _controls;
    public bool IsInventoryOpen { get; private set; } = false;

    bool _inventoryState = false;
    bool _playerState = true;

    protected override void Awake()
    {
        base.Awake();
        _controls = new PlayerControls();
    }

    void OnEnable()
    {
        _controls?.Enable();

        if (!_inventoryState)
        {
            _controls.Inventory.Disable(); // Disable inventory controls if they were previously disabled
        }

        if (!_playerState)
        {
            _controls.Player.Disable(); // Disable player controls if they were previously disabled
        }
    }

    void OnDisable()
    {
        Debug.Log("InputManager OnDisable called");

        _inventoryState = _controls.Inventory.enabled;
        _controls.Inventory.Disable();

        _playerState = _controls.Player.enabled;
        _controls.Player.Disable();

        _controls?.Disable();
    }

    public void ToggleInventory()
    {
        IsInventoryOpen = !IsInventoryOpen;

        if (IsInventoryOpen)
        {
            // Open the inventory UI
            Time.timeScale = 0; // Pause the game
            InventoryManager.Instance.ShowInventory(true); // Show the inventory UI
            _controls.Player.Disable(); // Disable player controls when inventory is open
            _controls.Inventory.Enable(); // Enable inventory controls if needed
        }
        else
        {
            // Close the inventory UI
            _controls.Player.Enable(); // Re-enable player controls when inventory is closed
            _controls.Inventory.Disable(); // Disable inventory controls
            InventoryManager.Instance.ShowInventory(false); // Hide the inventory UI
            Time.timeScale = 1; // Resume the game
        }
    }
}
