using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    PlayerControls _controls;
    Vector2 _movement;

    public bool IsLookingRight {get; private set;} = true;
    public bool IsAttacking {get; private set;} = false;

    void Awake()
    {
        _controls = new PlayerControls();
        _controls.Player.Attack.performed += context => Attack(context);

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
        rb.MovePosition(rb.position + _movement.normalized * moveSpeed * Time.fixedDeltaTime);
        sr.flipX = !IsLookingRight;
    }

    void Attack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

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
}
