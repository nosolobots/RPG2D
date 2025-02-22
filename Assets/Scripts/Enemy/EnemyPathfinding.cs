using System.Collections;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Rigidbody2D rb;
    PushBack pushBack;

    Vector2 _moveDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pushBack = GetComponent<PushBack>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (pushBack.IsPushed) return;
        
        rb.MovePosition(rb.position + _moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    public void MoveTo(Vector2 target)
    {
        _moveDirection = target;
    }
}
