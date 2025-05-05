using UnityEngine;

public class PortalSpawn : MonoBehaviour
{
    [SerializeField] string portalName;

    void Start()
    {
        if (SceneManagement.Instance.destPortalName == portalName)
        {
            PlayerController.Instance.transform.position = transform.position;
            CameraManager.Instance.SetPlayerCameraFollow();
        }
    }
}
