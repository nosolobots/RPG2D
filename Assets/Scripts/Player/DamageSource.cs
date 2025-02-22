using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float pushForce = 10f;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyDamage>())
        {
            collision.gameObject.GetComponent<EnemyDamage>().Hit(damage);
        }

        if (collision.gameObject.GetComponent<PushBack>())
        {
            collision.gameObject.GetComponent<PushBack>().Push(
                (collision.transform.position - transform.position).normalized, 
                pushForce
            );
        }
    }
}
