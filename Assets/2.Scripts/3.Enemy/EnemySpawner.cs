using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float spawnInterval = 2f;

    [SerializeField] Camera mainCamera;
    [SerializeField] MapBounds mapBounds;

    [SerializeField] int maxSpawnAttempts = 20;
    [SerializeField] float cameraPadding = 1.5f;

    float timer = 0f;

    private void Start()
    {
        SpawnEnemy();
    }

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
        if (player == null || mainCamera == null || mapBounds == null) return;

        for (int i = 0; i < maxSpawnAttempts; i++)
        {
            Vector2 spawnPos = GetRandomPositionOutsideCamera();

            if (!IsInsideMap(spawnPos)) continue;

            PoolManager.instance.GetEnemy(spawnPos, player);
            return;
        }
    }

    bool IsInsideMap(Vector2 position)
    {
        return (position.x >= mapBounds.Min.x &&
                position.x <= mapBounds.Max.x &&
                position.y >= mapBounds.Min.y &&
                position.y <= mapBounds.Max.y);
    }

    Vector2 GetRandomPositionOutsideCamera()
    {
        Vector2 camPos = mainCamera.transform.position;

        float halfHeight = mainCamera.orthographicSize;
        float halfWidth = halfHeight * mainCamera.aspect;

        halfHeight += cameraPadding;
        halfWidth += cameraPadding;

        float left = camPos.x - halfWidth;
        float right = camPos.x + halfWidth;
        float bottom = camPos.y - halfHeight;
        float top = camPos.y + halfHeight;

        int side = Random.Range(0, 4);

        switch (side)
        {
            case 0: // 위
                return new Vector2(Random.Range(left, right), top);

            case 1: // 아래
                return new Vector2(Random.Range(left, right), bottom);

            case 2: // 왼쪽
                return new Vector2(left, Random.Range(bottom, top));

            default: // 오른쪽
                return new Vector2(right, Random.Range(bottom, top));
        }
    }
}
