using UnityEngine;
using UnityEngine.InputSystem;

public class EnemySpawner : MonoBehaviour // 언제, 어떤 몬스터를 얼마나 자주 등장시킬 것인가?
{
    [SerializeField] Transform player;
    [SerializeField] Camera mainCamera;
    [SerializeField] MapBounds mapBounds;

    [SerializeField] float cameraPadding = 1.5f;
    [SerializeField] int maxSpawnAttempts = 20;
    [SerializeField] float spawnInterval = 2f;

    [SerializeField] float firstDifficultyTime = 60f;
    [SerializeField] float secondDifficultyTime = 120f;
    [SerializeField] float thirdDifficultyTime = 180f;

    [SerializeField] float firstSpawnInterval = 2f;
    [SerializeField] float secondSpawnInterval = 1.7f;
    [SerializeField] float thirdSpawnInterval = 1.5f;
    [SerializeField] float FourthSpawnInterval = 1.3f;

    float timer;
    int spawnCount = 1;

    int normalSpawnRate;
    int fastSpawnRate;
    int tankSpawnRate;

    private void Start()
    {
        UpdateDifficulty();
        SpawnEnemy();
    }

    private void Update()
    {
        if (GameManager.instance.isGameOver) return;

        timer += Time.deltaTime;

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

            PoolManager.instance.GetEnemy(spawnPos, player, SelectEnemyType());
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

    // 카메라 밖 몬스터 스폰 위치 랜덤 설정
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

    // 구간별 난이도 설정
    void UpdateDifficulty()
    {
        if (GameManager.instance.survivalTime < firstDifficultyTime) // 0 ~ 1분
        {
            spawnInterval = firstSpawnInterval;
            spawnCount = 1;

            normalSpawnRate = 100;
            fastSpawnRate = 0;
            tankSpawnRate = 0;
        }
        else if (GameManager.instance.survivalTime < secondDifficultyTime) // 1 ~ 2분
        {
            spawnInterval = secondSpawnInterval;
            spawnCount = 2;

            normalSpawnRate = 75;
            fastSpawnRate = 25;
            tankSpawnRate = 0;
        }
        else if (GameManager.instance.survivalTime < thirdDifficultyTime) // 2 ~ 3분
        {
            spawnInterval = thirdSpawnInterval;
            spawnCount = 2;

            normalSpawnRate = 60;
            fastSpawnRate = 25;
            tankSpawnRate = 15;
        }
        else                                            // 3 ~ 4분
        {
            spawnInterval = FourthSpawnInterval;
            spawnCount = 3;

            normalSpawnRate = 45;
            fastSpawnRate = 35;
            tankSpawnRate = 20;
        }
    }

    EnemyType SelectEnemyType()
    {
        int num = Random.Range(0, 100);

        if (num < normalSpawnRate) return EnemyType.Normal;
        else if (num < normalSpawnRate + fastSpawnRate) return EnemyType.Fast;
        else return EnemyType.Tank;
    }
}
