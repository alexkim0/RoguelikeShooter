using UnityEngine;

public class ChestEnemySpawner : RandomSpawner
{
    [Header("Spawn Settings")]
    public int lowBoundEnemyCount = 4;
    public int highBoundEnemyCount = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }   

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void SpawnEnemies()
    {
        spawnCount = Random.Range(lowBoundEnemyCount, highBoundEnemyCount);
        base.SpawnEnemies();
    }
}
