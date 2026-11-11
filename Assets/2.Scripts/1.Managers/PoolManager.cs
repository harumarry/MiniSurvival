using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour // 몬스터를 어떻게 제공하고 관리할 것인가?
{
    public static PoolManager instance;

    [SerializeField] Projectile projectilePrefab;
    [SerializeField] int projectilePoolSize = 20;

    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] int enemyPoolSize = 20;

    Queue<Projectile> projectilePool = new Queue<Projectile>();
    Queue<GameObject>[] enemyPools = new Queue<GameObject>[3];

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
        for (int i = 0; i < enemyPools.Length; i++)
        {
            enemyPools[i] = new Queue<GameObject>();

            for (int j = 0; j < enemyPoolSize; j++)
            {
                GameObject enemy = CreateEnemy((EnemyType)i);
                enemyPools[i].Enqueue(enemy);
            }
        }      
    }

    GameObject CreateEnemy(EnemyType type)
    {
        GameObject enemy = Instantiate(enemyPrefabs[((int)type)], transform);
        enemy.SetActive(false);
        enemy.GetComponent<EnemyHealth>().enemyType = type;

        return enemy;
    }

    public GameObject GetEnemy(Vector2 position, Transform target, EnemyType type)
    {
        GameObject enemy;

        if (enemyPools[(int)type].Count > 0)
        {
            enemy = enemyPools[(int)type].Dequeue();
        }
        else
        {
            enemy = CreateEnemy(type);
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

    public void ReturnEnemy(GameObject enemy, EnemyType type)
    {
        enemy.SetActive(false);
        enemyPools[(int)type].Enqueue(enemy);
    }
}
