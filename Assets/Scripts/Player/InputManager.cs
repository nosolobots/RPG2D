using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    PlayerControls _controls;

    public PlayerControls Controls => _controls;
    public bool IsInventoryOpen { get; private set; } = false;

    protected override void Awake()
    {
        base.Awake();
        _controls = new PlayerControls();
        _controls.Enable();
        _controls.Player.Enable(); // Ensure player controls are enabled by default
        _controls.Inventory.Disable(); // Disable inventory controls by default
    }

    void OnEnable()
    {
        _controls?.Enable();
    }

    void OnDisable()
    {
        _controls?.Disable();
    }

    public void ToggleInventory()
    {
        IsInventoryOpen = !IsInventoryOpen;

        if (IsInventoryOpen)
        {
            // Open the inventory UI
            _controls.Player.Disable(); // Disable player controls when inventory is open
            _controls.Inventory.Enable(); // Enable inventory controls if needed
            Time.timeScale = 0; // Uncomment if you want to pause the game
        }
        else
        {
            // Close the inventory UI
            _controls.Player.Enable(); // Re-enable player controls when inventory is closed
            _controls.Inventory.Disable(); // Disable inventory controls
            Time.timeScale = 1; // Uncomment if you want to resume the game
        }
    }
}
