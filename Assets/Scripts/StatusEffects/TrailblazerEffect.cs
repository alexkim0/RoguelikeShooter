using UnityEngine;

public class TrailblazerEffect : MonoBehaviour
{
    public PlayerMovement playerMovement;   // drag your PlayerMovement here
    public GameObject firePrefab;           // drag your FireTrailPiece prefab here
    public float spawnInterval = 0.1f;      // how often to drop fire
    public float fireLifetime = 1.5f;       // how long the fire stays

    private float spawnTimer = 0f;

	void Start()
	{
		
	}
    void Update()
    {
        if (playerMovement != null && playerMovement.dashing)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0f)
            {
                SpawnFire();
                spawnTimer = spawnInterval;
            }
        }
        else
        {
            // reset so dash starts dropping immediately next time
            spawnTimer = 0f;
        }
    }

    void SpawnFire()
    {
        // drop fire at player’s	 feet – adjust offset if needed
        Vector3 pos = transform.position;
        GameObject fire = Instantiate(firePrefab, pos, Quaternion.identity);
		fire.gameObject.GetComponent<FireDamage>().damage *= 1.5f;
        Destroy(fire, fireLifetime);
		Debug.Log("fire !!");
    }
}
