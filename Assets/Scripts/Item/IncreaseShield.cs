using UnityEngine;

public class IncreaseShield : GeneralItem
{
    public float increaseAmount = 10f;
    public PlayerStats playerStats;

    protected override void Start()
    {
        base.Start();
        playerStats = player.GetComponent<PlayerStats>();
    }

    public override void giveItem()
    {
        playerStats.maxShield += increaseAmount;
        playerStats.currentShield += increaseAmount; 
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        if (playerStats != null)
            {
                giveItem();
            }
        }
    }


}