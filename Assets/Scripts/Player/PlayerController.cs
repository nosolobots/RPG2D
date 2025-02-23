using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashTime = .2f;

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    PlayerControls _controls;
    Vector2 _movement;
    float _speed;

    public bool IsLookingRight {get; private set;} = true;
    public bool IsAttacking {get; private set;} = false;

    void Awake()
    {
        _controls = new PlayerControls();
        _controls.Player.Attack.performed += _ => Attack();
        _controls.Player.Dash.performed += _ => Dash();

        _speed = moveSpeed;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        _controls.Enable();
    }

    void OnDisable()
    {
        _controls.Disable();
    }

    void Update()
    {
        ReadInput();
    }

    void FixedUpdate()
    {
        Move();
    }

    void ReadInput()
    {
        _movement = _controls.Player.Move.ReadValue<Vector2>();
        anim.SetFloat("moveX", _movement.x);
        anim.SetFloat("moveY", _movement.y);
        if (_movement.x != 0) IsLookingRight = _movement.x > 0;
    }

    void Move()
    {
        rb.MovePosition(rb.position + _movement.normalized * _speed * Time.fixedDeltaTime);
        sr.flipX = !IsLookingRight;
    }

    void Attack()
    {
        anim.SetTrigger("attack");
    }

    void OnAttackStart()
    {
        IsAttacking = true;
    }

    void OnAttackEnd()
    {
        IsAttacking = false;
    }

    void Dash()
    {
        _speed = dashSpeed;
        
        StartCoroutine(DashCooldown());
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashTime);

        _speed = moveSpeed;
    }
}
