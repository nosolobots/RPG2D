using System.Collections;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Rigidbody2D rb;

    Vector2 _moveDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.MovePosition(rb.position + _moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    public void MoveTo(Vector2 target)
    {
        _moveDirection = target;
    }


}
