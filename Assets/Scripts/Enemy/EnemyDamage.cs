using System.Collections;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int initialHealth;

    int currentHealth;

    void Start()
    {
        currentHealth = initialHealth;
    }

    public void Hit(int damage)
    {
        currentHealth -= damage;

        StartCoroutine(HitEffect());
    }

    IEnumerator HitEffect()
    {
        Material material = GetComponent<SpriteRenderer>().material;
        material.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        material.color = Color.white;

        CheckDeath();
    }

    void CheckDeath()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
