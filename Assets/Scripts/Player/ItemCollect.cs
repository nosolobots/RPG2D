using UnityEngine;

public class ItemCollect : MonoBehaviour
{
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

            // Activamos el arma en el jugador
            ActiveWeapon activeWeapon = other.GetComponent<ActiveWeapon>();
            if (activeWeapon != null)
            {
                activeWeapon.SetActiveWeapon(itemName);
            }

            // Destruimos el objeto del mundo
            Destroy(gameObject);
        }
    }
}
