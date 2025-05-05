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

            // Set the player's camera to follow the player
            CameraManager.Instance.SetPlayerCameraFollow();

            // Fade from black
            UIFade.Instance.FadeFromBlack();
        }
    }   
}
