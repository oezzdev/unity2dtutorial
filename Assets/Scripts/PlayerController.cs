using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float MoveSpeed = 2f;
    public float JumpForce = 3f;
    public float ShootCooldown = 0.5f;
    private Rigidbody2D rb;
    private Animator animator;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction shootAction;
    private bool isGrounded;
    private float lastShootTime;
    private int health = 100;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        moveAction = InputSystem.actions.FindAction("Move", true);
        jumpAction = InputSystem.actions.FindAction("Jump", true);
        shootAction = InputSystem.actions.FindAction("Attack", true);
    }

    private void Update()
    {
        float horizontalInput = moveAction.ReadValue<Vector2>().x;
        animator.SetBool("Running", horizontalInput != 0);
        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.11f);
        if (jumpAction.triggered && isGrounded)
        {
            OnJumpPerformed();
        }

        if (shootAction.triggered && Time.time > lastShootTime + ShootCooldown)
        {
            OnShotPerformed();
        }
    }

    void FixedUpdate()
    {
        float horizontalInput = moveAction.ReadValue<Vector2>().x;
        rb.linearVelocity = new Vector2(horizontalInput * MoveSpeed, rb.linearVelocity.y);
    }

    private void OnJumpPerformed()
    {
        rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }

    private void OnShotPerformed()
    {
        Vector3 shootDirection = transform.localScale.x > 0 ? Vector3.right : Vector3.left;
        GameObject bullet = Instantiate(BulletPrefab, transform.position + shootDirection * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletController>().SetDirection(shootDirection);
        lastShootTime = Time.time;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
