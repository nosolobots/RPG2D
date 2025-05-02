using System.Collections;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Rigidbody2D rb;
    SpriteRenderer sr;
    PushBack pushBack;
    Animator anim;

    Vector2 _moveDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        pushBack = GetComponent<PushBack>();
    }

    void Update()
    {
        SetAnimation();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (pushBack.IsPushed) return;
        
        rb.MovePosition(rb.position + 
            _moveDirection * moveSpeed * Time.fixedDeltaTime);

        sr.flipX = _moveDirection.x < 0;
    }

    public void MoveTo(Vector2 targetDir)
    {
        _moveDirection = targetDir;
    }

    void SetAnimation()
    {
        anim.SetFloat("Move", _moveDirection.magnitude);
        sr.flipX = _moveDirection.x < 0;
    }
}
