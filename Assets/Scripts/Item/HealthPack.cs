using UnityEngine;

public class HealthPack : GeneralItem
{
    public float healAmount = 25f;
    private PlayerStats playerStats;

    protected override void Start()
    {
        base.Start();
        playerStats = player.GetComponent<PlayerStats>();
    }

    public override void giveItem()
    {
        playerStats.currentHealth += healAmount;
        playerStats.currentHealth = Mathf.Min(playerStats.currentHealth, playerStats.maxHealth);
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