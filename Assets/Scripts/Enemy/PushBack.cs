using System.Collections;
using UnityEngine;

public class PushBack : MonoBehaviour
{
    Rigidbody2D rb;
    public bool IsPushed {get; private set;} = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Push(Vector2 direction, float force)
    {
        IsPushed = true;
        rb.AddForce(direction * force * rb.mass, ForceMode2D.Impulse);
        StartCoroutine(StopPush());
    }

    IEnumerator StopPush()
    {
        yield return new WaitForSeconds(0.25f);
        IsPushed = false;
    }
}
