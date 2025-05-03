using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<DamageSource>())
        {
            Invoke("Destruction", 0.2f);
        }
    }

    void Destruction()
    {
        Instantiate(deathVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
