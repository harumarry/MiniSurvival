using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float lifeTime = 3f;

    Vector2 direction;
    float damage;
    float lifeTimer;
    bool isReturned;

    public void Init(Vector2 direction, float damage)
    {
        this.direction = direction;
        this.damage = damage;

        lifeTimer = 0f;
        isReturned = false;
    }

    private void Update()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifeTime)
        {
            ReturnToPool();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
            ReturnToPool();
        }
    }

    void ReturnToPool()
    {
        if (isReturned) return;

        isReturned = true;
        PoolManager.instance.ReturnProjectile(this);
    }
}
