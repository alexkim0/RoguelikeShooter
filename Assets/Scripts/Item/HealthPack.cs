using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public float healAmount = 25f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();

        if (playerStats != null)
            {
                playerStats.currentHealth += healAmount;
                playerStats.currentHealth = Mathf.Min(playerStats.currentHealth, playerStats.maxHealth);
                Destroy(gameObject);
            }
        }
    }
}