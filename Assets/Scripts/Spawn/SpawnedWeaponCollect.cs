using UnityEngine;

public class SpawnedWeaponCollect : SpawnedItemCollect
{
    protected override void UpdateItemState()
    {
        // Obtenemos el SpawnOnceWeaponSO asociado al objeto
        SpawnOnceWeaponSO weaponData = ResourcesManager.Instance.GetResource(gameObject.name) as SpawnOnceWeaponSO;
        if (weaponData == null)
        {
            Debug.LogError($"No se encontró SpawnOnceWeaponSO para el objeto {gameObject.name}");
            return;
        }

        // Mostramos un mensaje de recogida
        if (!string.IsNullOrEmpty(weaponData.message))
        {
            MessageManager.Instance.ShowMessage(weaponData.message + " Se añadió a tu inventario.");
        }

        // Instanciamos el arma como hijo del jugador si no la tiene ya
        if (!PlayerController.Instance.GetComponent<ActiveWeapon>().hasWeapon(weaponData.itemID))
        {
            // Instanciamos el prefab del arma como hijo del jugador
            Transform playerTransform = PlayerController.Instance.transform;
            GameObject weapon = Instantiate(weaponData.weaponPrefab, playerTransform.position, Quaternion.identity, playerTransform);

            // Parametrizamos el arma 
            weapon.name = weaponData.itemID; // Asignamos el nombre del arma
            weapon.GetComponent<DamageSource>().SetWeaponData(
                weaponData.damage,
                weaponData.pushForce
            );
        }

        // Añadimos el arma al inventario del jugador
        InventoryManager.Instance.AddItem(weaponData.itemID, weaponData.weaponSprite);
        
        // Actualizamos el estado del item en ResourcesManager a Collected
        // y destruimos el objeto de la escena
        base.UpdateItemState();
    }
}
