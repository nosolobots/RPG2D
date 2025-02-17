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
    public bool _lookingRight {get; private set;} = true;

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
        //Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y, transform.position.z), 
        //                  new Vector3(_lookingRight ? 1 : -1, _movement.y, transform.position.z));

        Ray ray = new Ray(transform.position, _lookingRight ? Vector3.right : -Vector3.right); 

        Debug.DrawRay(ray.origin, 1 * ray.direction, Color.red, 0.5f, false);

        // crea un layerMask para que el raycast incluya todos los objetos de la escena
        LayerMask layerMask = 0;
        
        /*
        RaycastHit2D[] hitEnemies = Physics2D.GetRayIntersectionAll(ray);
        
        Debug.Log("Enemies hit: " + hitEnemies.Length);

        foreach (RaycastHit2D enemy in hitEnemies)
        {
            Debug.Log("Enemy hit: " + enemy.collider.gameObject.name);
            if (enemy.collider != null)
            {
                Destroy(enemy.collider.gameObject);
            }
        }
        */
        layerMask = LayerMask.GetMask("Enemies");
        Vector3 attackDirection = new Vector3(_lookingRight ? 1 : -1, _movement.y, 0).normalized;
        float attackDistance = 1f;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, attackDirection, attackDistance, layerMask);
        if (hit)
        {
            GameObject enemy = hit.collider.gameObject;
            Debug.Log("Enemy hit: " + enemy.name);
            //Destroy(enemy);
            //Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
            //Vector2 pushDirection = new Vector2(attackDirection.x, attackDirection.y);
            //enemyRb.AddForce(enemy.transform.right, ForceMode2D.Impulse);
            //enemyRb.MovePosition(enemyRb.position + pushDirection * 100f);
            enemy.GetComponent<EnemyDamage>().Hit(new Vector2(attackDirection.x, attackDirection.y));
        }
    }
}
