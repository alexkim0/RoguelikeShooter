using UnityEngine;

public class IncreaseShield : GeneralItem
{
    public float increaseAmount = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();

        if (playerStats != null)
            {
                playerStats.maxShield += increaseAmount;
                playerStats.currentShield += increaseAmount; 
                Destroy(gameObject);
            }
        }
    }
}