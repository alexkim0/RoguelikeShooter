using UnityEngine;

public class HealthIncrease : GeneralItem
{
    public float increaseAmount = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();

        if (playerStats != null)
            {
                playerStats.maxHealth += increaseAmount;
                playerStats.currentHealth += increaseAmount; 
                Destroy(gameObject);
            }
        }
    }
}