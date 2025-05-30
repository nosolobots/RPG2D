using UnityEngine;

public class SpawnOnceSpawner : MonoBehaviour
{
    [SerializeField] SpawnOncePoint[] spawnPoints;
    void Start()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            if (!SpawnOnceManager.Instance.IsDestroyed(spawnPoint.id))
            {
                Instantiate(spawnPoint.prefab, spawnPoint.transform.position, Quaternion.identity,
                    // parent
                    spawnPoint.gameObject.transform);
            }
        }
    }

}
