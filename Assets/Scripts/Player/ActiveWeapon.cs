using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    GameObject currentWeapon;
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
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
}
