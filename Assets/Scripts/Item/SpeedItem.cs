using UnityEngine;

public class SpeedItem : GeneralItem
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject player;
    public float multiplier;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

//    public float walkSpeed;
    // public float maxAirSpeed;
    // public float downwardSlideSpeed;
    // public float slideSpeed;
    // private float moveSpeed;
    // public float dashSpeed;
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
        // movement.moveSpeed *= multiplier;

	}


}
