using System.ComponentModel.Design;
using Unity.VisualScripting;
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
    bool _lookingRight = true;

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
        if (_movement.x != 0) _lookingRight = _movement.x > 0;
    }

    void Move()
    {
        rb.MovePosition(rb.position + _movement.normalized * moveSpeed * Time.fixedDeltaTime);
        sr.flipX = !_lookingRight;
    }

    void Attack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        anim.SetTrigger("attack");

        CheckForEnemies();
    }

    void CheckForEnemies()
    {
        Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y, 0f), 
                            new Vector3(_lookingRight ? 1 : -1, _movement.y, 0f));

        //Debug.DrawRay(ray.origin, 1 * ray.direction, Color.red, 0.5f, false);

        int layerMask = LayerMask.GetMask("Enemies");
        RaycastHit2D[] hitEnemies = Physics2D.GetRayIntersectionAll(ray, 2f, layerMask);

        foreach (RaycastHit2D enemy in hitEnemies)
        {
            Debug.Log("Enemy hit: " + enemy.collider.gameObject.name);
            if (enemy.collider != null)
            {
                Destroy(enemy.collider.gameObject);
            }
        }
    }
}
