using UnityEngine;

[CreateAssetMenu(fileName = "SpawnOnceWeaponSO", menuName = "Scriptable Objects/SpawnOnceWeaponSO")]
public class SpawnOnceWeaponSO : SpawnOnceSO
{
    [Header("Weapon Properties")]
    public int damage; // Damage dealt by the weapon
    public float pushForce; // Force applied to enemies when hit

    [Header("Weapon Visuals")]
    public Sprite weaponSprite; // Sprite representing the weapon for UI

    [Header("Player Weapon")]
    public string weaponName; // Name of the weapon
    public GameObject weaponPrefab; // Prefab of the weapon to spawn in the player's inventory
    public string animatorFlag; // Animation flag for the weapon

    [Header("Firing Weapon")]
    public bool firingWeapon; // Flag to indicate if the weapon is currently firing
    public GameObject projectilePrefab; // Prefab of the projectile to spawn when firing

    private void OnValidate()
    {
        this.itemType = SpawnOnceType.Weapon; // Ensure the item type is set to Weapon
    }
}
