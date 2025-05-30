using UnityEngine;

public class SpawnOnceUpdate : MonoBehaviour
{
    public void UpdateState()
    {
        SpawnOncePoint spawnPoint = GetComponentInParent<SpawnOncePoint>();
        if (spawnPoint != null)
        {
            SpawnOnceManager.Instance.MarkAsDestroyed(spawnPoint.id);
        }        
    }
}
