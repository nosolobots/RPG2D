using System.Collections;
using TMPro;
using UnityEngine;

public class MessageManager : Singleton<MessageManager>, IPlayerCollectObserver
{
    [SerializeField] float charDelay;
    [SerializeField] TextMeshProUGUI txtMessage;
    [SerializeField] GameObject messagePanel;

    PlayerControls _controls;

    protected override void Awake()
    {
        base.Awake();

    }

    void Start()
    {
        _controls = InputManager.Instance.Controls;
        _controls.UI.Close.performed += _ => HideMessage();

        // Nos registramos como observador del PlayerCollectSubject
        PlayerCollectSubject.Instance.AddObserver(this);
    }

    public void ShowMessage(string message)
    {
        txtMessage.text = string.Empty; // Clear previous text

        Time.timeScale = 0; // Pause the game

        messagePanel.SetActive(true); // Show the message panel

        StartCoroutine(TypeText(message));
    }

    void HideMessage()
    {
        StopAllCoroutines(); // Stop any ongoing text typing coroutine
        messagePanel.SetActive(false); // Hide the message panel

        Time.timeScale = 1; // Resume the game
    }

    private IEnumerator TypeText(string message)
    {
        foreach (char letter in message)
        {
            txtMessage.text += letter; // Append each character

            // Wait for the specified delay (no se detiene al pausar el juego)
            yield return new WaitForSecondsRealtime(charDelay); 
        }
    }

    public void OnNotify(string itemID)
    {
        // Mostramos el mensaje de recogida
        SpawnOnceSO resource = ResourcesManager.Instance.GetResource(itemID);
        if (resource != null && !string.IsNullOrEmpty(resource.message))
        {
            string message = resource.message;

            if (resource.itemType == SpawnOnceSO.SpawnOnceType.Weapon)
            {
                message += " Se añadió a tu inventario.";
            }
            
            ShowMessage(resource.message);
        }
    }
}
