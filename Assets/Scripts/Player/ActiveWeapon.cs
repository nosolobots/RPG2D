using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    GameObject currentWeapon;
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SetActiveWeapon(string weaponName)
    {
        if (currentWeapon != null)
        {
            currentWeapon.SetActive(false);
            anim.SetBool(currentWeapon.name.ToLower(), false);
        }

        GameObject weapon = transform.Find("Weapons/" + weaponName)?.gameObject;
        currentWeapon = weapon;
        currentWeapon.SetActive(true);
        anim.SetBool(weaponName.ToLower(), true);
    }
}
