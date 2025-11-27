using UnityEngine;

public class MapEnemySpawner : EnemySpawner
{
    [Header("Spawn Settings")]
    public float spawnTimeInterval = 10f;
    private float spawnTimer = 0f;
    float currentTime = 0f;
    float nextIncreaseTime = 30f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnEnemies();
        spawnTimer = spawnTimeInterval;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;
        spawnTimer -= Time.deltaTime;

        IncreaseSpawnedEnemies();

        if (spawnTimer <= 0)
        {
            SpawnEnemies();
            spawnTimer = spawnTimeInterval;
        }
    }

    void IncreaseSpawnedEnemies()
    {
        if (currentTime >= nextIncreaseTime)
        {
            nextIncreaseTime += 30f;
            enemyCount += 5;
            Debug.Log("Increasing enemy count to: " + enemyCount);
        }
    }
}
