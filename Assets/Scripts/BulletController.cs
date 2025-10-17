using UnityEngine;

public class BulletController : MonoBehaviour
{
    public AudioClip ShotSound;
    public float Speed = 10f;
    private Vector2 Direction;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(ShotSound);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = Direction * Speed;
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction.normalized;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerController? controller = collider.GetComponent<PlayerController>();
        if (controller is not null)
        {
            controller.TakeDamage(20);
            Destroy();
            return;
        }
        
        GruntController? gruntController = collider.GetComponent<GruntController>();
        if (gruntController is not null)
        {
            gruntController.TakeDamage(20);
            Destroy();
            return;
        }

        Destroy();
    }
}
