using System.Collections;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int initialHealth;
    [SerializeField] float hitTime = 0.1f;
    [SerializeField] GameObject deathVFX;
    Material material;

    int currentHealth;

    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;

        currentHealth = initialHealth;
    }

    public void Hit(int damage)
    {
        currentHealth -= damage;

        StartCoroutine(HitEffect());
    }

    IEnumerator HitEffect()
    {
        material.color = Color.red;

        yield return new WaitForSeconds(hitTime);

        material.color = Color.white;

        CheckDeath();
    }

    void CheckDeath()
    {
        if (currentHealth <= 0)
        {
            Instantiate(deathVFX, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }
    }
}
