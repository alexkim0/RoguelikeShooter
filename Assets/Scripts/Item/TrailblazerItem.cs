using UnityEngine;

public class TrailblazerItem : GeneralItem
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
	public PlayerMovement playerMovement;
    public GameObject firePrefab;
    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
		
    }


    public override void giveItem()
	{
        playerMovement = GetComponent<PlayerMovement>();
        TrailblazerEffect buff = player.AddComponent<TrailblazerEffect>();
        buff.playerMovement = player.GetComponent<PlayerMovement>();
        buff.firePrefab = firePrefab;   // assign via inspector or Resources.Load
        Destroy(gameObject);
	}


}
