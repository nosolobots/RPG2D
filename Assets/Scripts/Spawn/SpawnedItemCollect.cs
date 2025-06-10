using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class SpawnedItemCollect : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerCollectSubject>()?.NotifyObservers(gameObject.name);

            // Después de notificar a los observadores, destruimos el objeto
            Destroy(gameObject);
        }
    }
}
