using UnityEngine;

public class DamageSource : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyAI>())
        {
            Debug.Log("Enemy hit!");
        }
    }
}
