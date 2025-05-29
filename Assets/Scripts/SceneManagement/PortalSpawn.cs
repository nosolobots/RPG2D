using UnityEngine;

public class PortalSpawn : MonoBehaviour
{
    [SerializeField] string portalName;

    void Start()
    {
        if (SceneManagement.Instance.destPortalName == portalName)
        {
            // Set the player's position to the portal's position
            PlayerController.Instance.transform.position = transform.position;

            // Enable player controls
            InputManager.Instance.Controls.Player.Enable();

            // Enable player sprite
            PlayerController.Instance.GetComponentInParent<SpriteRenderer>().enabled = true;

            // Set the player's camera to follow the player
            CameraManager.Instance.SetPlayerCameraFollow();

            // Fade from black
            UIFade.Instance.FadeFromBlack();
        }
    }   
}
