using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Camera mainCamera;
    [SerializeField] MapBounds mapBounds;

    [SerializeField] float cameraPadding = 1.5f;
    [SerializeField] int maxSpawnAttempts = 20;
    [SerializeField] float spawnInterval = 2f;

    [SerializeField] float firstDifficultyTime = 30f;
    [SerializeField] float secondDifficultyTime = 60f;

    [SerializeField] float firstSpawnInterval = 2f;
    [SerializeField] float secondSpawnInterval = 1.5f;
    [SerializeField] float thirdSpawnInterval = 1.2f;


    float timer;
    float difficultyTimer;
    int spawnCount = 1;

    private void Start()
    {
        SpawnEnemy();
    }

    private void Update()
    {
        if (GameManager.instance.isGameOver) return;

        timer += Time.deltaTime;
        difficultyTimer += Time.deltaTime;

        UpdateDifficulty();

        if (timer >= spawnInterval)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                SpawnEnemy();
            }

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

    void UpdateDifficulty()
    {
        if (difficultyTimer < firstDifficultyTime)
        {
            spawnInterval = firstSpawnInterval;
            spawnCount = 1;
        }
        else if (difficultyTimer < secondDifficultyTime)
        {
            spawnInterval = secondSpawnInterval;
            spawnCount = 1;
        }
        else
        {
            spawnInterval = thirdSpawnInterval;
            spawnCount = 2;
        }
    }
}
