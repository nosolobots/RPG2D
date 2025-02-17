using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] PolygonCollider2D weaponColliderRight;
    [SerializeField] PolygonCollider2D weaponColliderLeft;
    [SerializeField] PlayerController playerController;

    void Start()
    {
        
    }

    void Update()
    {
        OrientateCollider();
    }

    void OrientateCollider()
    {
        //Debug.Log(playerController._lookingRight);
        weaponColliderRight.enabled = playerController._lookingRight;
        weaponColliderLeft.enabled = !playerController._lookingRight;
    }
}
