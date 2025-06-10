using UnityEngine;

public class ActiveWeapon : MonoBehaviour, IPlayerCollectObserver
{
    GameObject currentWeapon;
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        // Nos registramos como observador del PlayerCollectSubject
        PlayerCollectSubject.Instance.AddObserver(this);
    }

    public bool hasWeapon(string weaponName)
    {
        return transform.Find(weaponName) != null;
    }

    public void SetActiveWeapon(string weaponName)
    {
        if (currentWeapon != null)
        {
            currentWeapon.SetActive(false);

            // Desactivamos el bool del arma anterior en el animator
            anim.SetBool(
                ((SpawnOnceWeaponSO)ResourcesManager.Instance.GetResource(currentWeapon.name))?.animatorFlag,
                false);
        }

        // Activamos el nuevo arma
        currentWeapon = transform.Find(weaponName)?.gameObject;
        currentWeapon.SetActive(true);
        anim.SetBool(
            ((SpawnOnceWeaponSO)ResourcesManager.Instance.GetResource(currentWeapon.name))?.animatorFlag,
            true);
    }

    public void OnNotify(string itemID)
    {
        // Obtenemos el SpawnOnceWeaponSO asociado al objeto
        SpawnOnceSO data = ResourcesManager.Instance.GetResource(itemID) as SpawnOnceSO;
        if (data == null)
        {
            Debug.LogError($"No se encontró SpawnOnceWeaponSO para el item {itemID}");
            return;
        }

        // Si es un arma, la añadimos al inventario del jugador
        if (data.itemType == SpawnOnceSO.SpawnOnceType.Weapon)
        {
            SpawnOnceWeaponSO weaponData = data as SpawnOnceWeaponSO;

            // Si el jugador ya tiene el arma, no hacemos nada
            if (hasWeapon(weaponData.itemID))
            {
                Debug.Log($"El jugador ya tiene el arma {weaponData.itemID}. No se añade de nuevo.");
                return;
            }

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
    }
}
