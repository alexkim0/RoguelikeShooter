using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject resourcePrefab;
    public float spawnChance;

    [Header("Raycast Settings")]
    public float distanceBetweenCheck;
    // heightOfCheck: Y position of the check plane
    // rangeOfCheck: the length of the raycast
    public float heightOfCheck = 45f, rangeOfCheck = 30f;
    public LayerMask whatIsGround;
    public Vector2 positivePosition, negativePosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnPosition();
    }
    
    private void SpawnPosition()
    {
        for (float x = negativePosition.x; x < positivePosition.x; x += distanceBetweenCheck)
        {
            for (float z = negativePosition.y; z < positivePosition.y; z += distanceBetweenCheck)
            {
                RaycastHit hit;
                if (Physics.Raycast(new Vector3(x, heightOfCheck, z), Vector3.down, out hit, rangeOfCheck, whatIsGround))
                {
                    if (spawnChance > Random.Range(0, 101))
                        Instantiate(resourcePrefab, hit.point, Quaternion.identity, transform);
                }
            }
        }
        
    }
}
