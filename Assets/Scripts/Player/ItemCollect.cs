using UnityEngine;
using UnityEngine.Animations;

public class ItemCollect : MonoBehaviour
{
    [TextArea(3, 10)]
    [SerializeField] string msg;
    string itemName;
    Sprite itemImage;
    void Awake()
    {
        itemImage = GetComponent<SpriteRenderer>().sprite;
        itemName = gameObject.tag;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Añadimos el item al inventario
            InventoryManager.Instance.AddItem(itemName, itemImage);

            // Mostramos un mensaje de recogida
            if (!string.IsNullOrEmpty(msg))
            {
                MessageManager.Instance.ShowMessage(msg);
            }            

            // Activamos el arma en el jugador
            if (other.TryGetComponent<ActiveWeapon>(out var activeWeapon))
            {
                activeWeapon.SetActiveWeapon(itemName);
            }

            // Marcamos el objeto como destruido en SpawnOnceManager
            if (TryGetComponent<SpawnOnceUpdate>(out var spawnOnceUpdate))
            {
                spawnOnceUpdate.UpdateState();
            }

            // Destruimos el objeto del mundo
            Destroy(gameObject);
        }
    }
}
