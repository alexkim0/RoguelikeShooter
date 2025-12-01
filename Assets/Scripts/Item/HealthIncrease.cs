using UnityEngine;

public class HealthIncrease : GeneralItem
{
    public float increaseAmount = 10f;
    private PlayerStats playerStats;

    protected override void Start()
    {
        base.Start();
        playerStats = player.GetComponent<PlayerStats>();
    }

    public override void giveItem()
    {
        playerStats.maxHealth += increaseAmount;
        playerStats.currentHealth += increaseAmount; 
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats playerStats = other.GetComponent<PlayerStats>();

        if (playerStats != null)
            {
                giveItem();
            }
        }
    }
}