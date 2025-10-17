using UnityEngine;

public class GruntController : MonoBehaviour
{
    public GameObject Objective;
    public GameObject BulletPrefab;
    public float DistanceToAttack = 1f;
    private float lastAttackTime = 0f;
    private float attackCooldown = 1f;
    private int health = 80;

    void Update()
    {
        if (Objective == null) return;

        Vector3 direction = Objective.transform.position - transform.position;
        if (direction.x >= 0)
        {
            transform.localScale = Vector3.one;
        } else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        float distance = Mathf.Abs(Objective.transform.position.x - transform.position.x);
        if (distance < DistanceToAttack && Time.time > lastAttackTime + attackCooldown)
        {
            Atack();
            lastAttackTime = Time.time;
        }
    }

    private void Atack()
    {
        Vector3 atackDirection = transform.localScale.x > 0 ? Vector3.right : Vector3.left;
        GameObject bullet = Instantiate(BulletPrefab, transform.position + atackDirection * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletController>().SetDirection(atackDirection);
        lastAttackTime = Time.time;
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
