using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;

    [SerializeField] Projectile projectilePrefab;
    [SerializeField] int projectilePoolSize = 20;

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int enemyPoolSize = 20;

    Queue<Projectile> projectilePool = new Queue<Projectile>();
    Queue<GameObject> enemyPool = new Queue<GameObject>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        CreateProjectilePool();
        CreateEnemyPool();
    }

    void CreateProjectilePool()
    {
        for (int i = 0; i < projectilePoolSize; i++)
        {
            Projectile projectile = CreateProjectile();
            projectilePool.Enqueue(projectile);
        }
    } 

    Projectile CreateProjectile()
    {
        Projectile projectile = Instantiate(projectilePrefab, transform);
        projectile.gameObject.SetActive(false);

        return projectile;
    }

    public Projectile GetProjectile(Vector2 position)
    {
        Projectile projectile;

        if (projectilePool.Count > 0)
        {
            projectile = projectilePool.Dequeue();
        }
        else
        {
            projectile = CreateProjectile();
        }

        projectile.transform.position = position;
        projectile.gameObject.SetActive(true);

        return projectile;
    }

    public void ReturnProjectile(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
        projectilePool.Enqueue(projectile);
    }

    void CreateEnemyPool()
    {
        for (int i = 0; i < enemyPoolSize; i++)
        {
            GameObject enemy = CreateEnemy();
            enemyPool.Enqueue(enemy);
        }
    }

    GameObject CreateEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform);
        enemy.SetActive(false);

        return enemy;
    }

    public GameObject GetEnemy(Vector2 position, Transform target)
    {
        GameObject enemy;

        if (enemyPool.Count > 0)
        {
            enemy = enemyPool.Dequeue();
        }
        else
        {
            enemy = CreateEnemy();
        }

        enemy.transform.position = position;
        
        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.player = target;
        }

        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.ResetHealth();
        }

        enemy.SetActive(true);
        return enemy;
    }

    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        enemyPool.Enqueue(enemy);
    }
}
