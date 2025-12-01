using UnityEngine;

public class SpeedItem : GeneralItem
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float multiplier;
    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void giveItem()
	{
        
		PlayerMovement movement = player.GetComponent<PlayerMovement>();
        Dashing dashing = player.GetComponent<Dashing>();
        movement.walkSpeed *= multiplier;
        movement.maxAirSpeed *= multiplier;
        movement.downwardSlideSpeed *= multiplier;
        movement.dashSpeedChangeFactor *= multiplier;
        movement.dashSpeed *= multiplier;
        movement.slideSpeed *= multiplier;
        dashing.dashForce *= multiplier;
        Destroy(gameObject);
        // movement.moveSpeed *= multiplier;

	}


}
