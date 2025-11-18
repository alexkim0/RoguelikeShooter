using UnityEngine;

public class PantsDownItem : GeneralItem
{
    public GameObject player;
    public float multiplier;
    public HitscanGunSystem gun;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

public override void giveItem()
{
    PlayerMovement movement = player.GetComponent<PlayerMovement>();
    Dashing dashing = player.GetComponent<Dashing>();

    HitscanGunSystem gun = GameObject.FindGameObjectWithTag("Gun").GetComponent<HitscanGunSystem>();

    PantsDownEffect buff = player.AddComponent<PantsDownEffect>();
    buff.movement = movement;
    buff.dashing = dashing;
    buff.gun = gun;
    buff.multiplier = multiplier;
}


}
