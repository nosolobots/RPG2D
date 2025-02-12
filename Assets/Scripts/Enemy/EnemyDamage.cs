using System.Collections;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public void Hit(Vector2 hitDirection)
    {
        Debug.Log("Auch!");
        
        StartCoroutine(HitEffect());
    }

    IEnumerator HitEffect()
    {
        Material material = GetComponent<SpriteRenderer>().material;
        material.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        material.color = Color.white;
    }
}
