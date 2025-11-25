using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] enemyPrefabs;
    public int enemyCount = 5;
    public Vector3 areaSize = new Vector3(10f, 0, 10f);

    [Header("Ground Detection")]
    public LayerMask whatIsGround;
    public float raycastHeight = 10f;
    public float groundOffset = 0.1f;

    private bool hasSpawned = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 randomPos = GetRandomGroundPosition();
            GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Instantiate(enemy, randomPos, Quaternion.identity);
        }
    }

    private Vector3 GetRandomGroundPosition()
    {
        // pick random point in local XZ area around spawner
        // multiplying both x and y by 0.5 that way the spawn point is in middle, not in corner
        Vector3 offset = new Vector3(
            Random.Range(-areaSize.x * 0.5f, areaSize.x * 0.5f),
            0f,
            Random.Range(-areaSize.z * 0.5f, areaSize.z * 0.5f)
        );

        // adding from transform.position so it is relative to the spawner position
        // and moves up by raycastHeight to detect ground
        Vector3 start = transform.position + offset + Vector3.up * raycastHeight;
        Vector3 end = Vector3.down;

        if (Physics.Raycast(start, end, out RaycastHit hit, raycastHeight * 2f, whatIsGround,
                QueryTriggerInteraction.Ignore))
        {
            return hit.point + (Vector3.up * groundOffset);
        }

        // fallback: just use spawner position if raycast fails
        // it would not spawn on ground level
        return transform.position + offset;

    }
}
