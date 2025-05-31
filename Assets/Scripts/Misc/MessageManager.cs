using System.Collections;
using TMPro;
using UnityEngine;

public class MessageManager : Singleton<MessageManager>
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
}
