using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float pushForce = 10f;
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Aplicamos daño al enemigo (si recibe daño)
        EnemyDamage enemyDamage = collision.gameObject.GetComponent<EnemyDamage>();
        enemyDamage?.Hit(damage);

        // Aplicamos desplazamiento al enemigo (si se mueve)
        PushBack pushBack = collision.gameObject.GetComponent<PushBack>();
        pushBack?.Push(
            (collision.transform.position - transform.position).normalized,
            pushForce
        );
    }
    
    public void SetWeaponData(int damage, float pushForce)
    {
        this.damage = damage;
        this.pushForce = pushForce;
    }
}
