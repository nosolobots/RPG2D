using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    [SerializeField] PolygonCollider2D weaponColliderRight;
    [SerializeField] PolygonCollider2D weaponColliderLeft;
    [SerializeField] PlayerController playerController;

    void Update()
    {
        UpdateWeaponColliderState();
    }

    void UpdateWeaponColliderState()
    {
        if (playerController.IsAttacking)
        {
            weaponColliderRight.enabled = playerController.IsLookingRight;
            weaponColliderLeft.enabled = !playerController.IsLookingRight;
        }
        else
        {
            weaponColliderRight.enabled = false;
            weaponColliderLeft.enabled = false;
        }
    }
}

