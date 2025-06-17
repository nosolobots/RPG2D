using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Singleton<PlayerController>
{
    // PlayerController is a singleton class that manages player movement and actions.
    // It uses Unity's new Input System for handling player input.
    // The class handles player movement, dashing, and attacking.
    // It also manages the player's animation state and sprite flipping based on movement direction.

    // Serialized fields allow customization of player movement speed and dash speed in the Unity Inspector.
    // The class uses Rigidbody2D for physics-based movement and Animator for animation control.
    // The SpriteRenderer is used to flip the player's sprite based on the direction of movement.

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
    
    ActiveWeapon _activeWeapon;

    public bool IsLookingRight { get; private set; } = true;
    public bool IsAttacking {get; private set;} = false;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        // Initialize the PlayerControls input system
        //_controls = new PlayerControls();
        _controls = InputManager.Instance.Controls;

        _controls.Player.Attack.performed += _ => Attack();
        _controls.Player.Dash.performed += _ => Dash();
        _controls.Player.Inventory.performed += _ => InputManager.Instance.ToggleInventory();

        _speed = moveSpeed;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        _activeWeapon = GetComponent<ActiveWeapon>();
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

        _activeWeapon.FireWeapon();
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
