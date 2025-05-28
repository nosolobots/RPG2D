using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    GameObject sword;
    GameObject bow;
    GameObject axe;
    GameObject currentWeapon;
    Animator anim;

    void Awake()
    {

        // Find the weapon GameObjects in the hierarchy
        sword = transform.Find("Weapons/Sword")?.gameObject;
        bow = transform.Find("Weapons/Bow")?.gameObject;
        axe = transform.Find("Weapons/Axe")?.gameObject;

        anim = GetComponent<Animator>();

        // Start with the sword as the active weapon
        SetActiveWeapon(sword);
    }

    public void SetActiveWeapon(GameObject weapon)
    {
        if (weapon == null)
        {
            return;
        }
        
        if (currentWeapon != null)
        {
            currentWeapon.SetActive(false);
            anim.SetBool(weapon.name.ToLower(), false);
        }

        currentWeapon = weapon;
        currentWeapon.SetActive(true);
        anim.SetBool(weapon.name.ToLower(), true);
        Debug.Log($"Active weapon set to: {weapon.name}");
    }
}
