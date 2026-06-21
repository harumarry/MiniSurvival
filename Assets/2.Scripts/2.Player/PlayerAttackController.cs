using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] float attackDamage = 1f;
    [SerializeField] float attackInterval = 1f;
    [SerializeField] float attackRange = 5f;

    [SerializeField] int projectileCount = 1;
    [SerializeField] float spreadAngle = 15f;

    float fireTimer;

    private void Update()
    {
        if (GameManager.instance.isGameOver) return;

        fireTimer += Time.deltaTime;
        if (fireTimer >= attackInterval)
        {
            Transform target = FindNearestEnemy();

            if (target != null)
            {
                Fire(target);
            }
            
            fireTimer = 0f;
        }
    }

    Transform FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Transform nearestEnemy = null;
        float nearestDistance = attackRange;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }

        return nearestEnemy;
    }

    void Fire(Transform target)
    {
        Vector2 baseDirection = (target.position - transform.position).normalized;

        float startAngle = -spreadAngle * (projectileCount - 1) / 2f;

        for (int i=0; i < projectileCount; i++)
        {
            float angle = startAngle + spreadAngle * i;
            Vector2 ShotDirection = Quaternion.Euler(0f, 0f, angle) * baseDirection;

            Projectile projectile = PoolManager.instance.GetProjectile(transform.position);

            if (projectile != null)
            {
                projectile.Init(ShotDirection, attackDamage);
            }
        }
   
    }

    public void IncreaseDamage(float value)
    {
        attackDamage += value;
    }

    public void IncreaseAttackSpeed(float value)
    {
        attackInterval *= value;
    }

    public void IncreaseProjectileCount(int value)
    {
        projectileCount += value;
    }
}
