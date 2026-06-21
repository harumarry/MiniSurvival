using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float spawnInterval = 2f;
    [SerializeField] float spawnDistance = 6f;

    float timer = 0f;

    private void Update()
    {
        if (GameManager.instance.isGameOver) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();           
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

        Vector2 spawnPos = (Vector2)player.position + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * spawnDistance;

        PoolManager.instance.GetEnemy(spawnPos, player);
    }
}
