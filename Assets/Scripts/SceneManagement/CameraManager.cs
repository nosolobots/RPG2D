using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    public void SetPlayerCameraFollow()
    {
        CinemachineCamera cam = FindFirstObjectByType<CinemachineCamera>();
        cam.Follow = PlayerController.Instance.transform;
    }
}
