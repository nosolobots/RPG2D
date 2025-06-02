using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class SpawnedItemCollect : MonoBehaviour
{
    protected virtual void UpdateItemState()
    {
        // Actualizamos el estado del item en ResourcesManager
        ResourcesManager.Instance.GetResource(gameObject.name).state = SpawnOnceSO.SpawnOnceState.Collected;

        // Destruimos el objeto del mundo
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UpdateItemState();
        }
    }
}
