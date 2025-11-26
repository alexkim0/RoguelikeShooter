using UnityEngine;

public class MapEnemySpawner : EnemySpawner
{
    [Header("Spawn Settings")]
    public float spawnTimeInterval = 10f;
    private float spawnTimer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnEnemies();
        spawnTimer = spawnTimeInterval;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            enemyCount = 1;
            SpawnEnemies();
            spawnTimer = spawnTimeInterval;
        }
    }
}
