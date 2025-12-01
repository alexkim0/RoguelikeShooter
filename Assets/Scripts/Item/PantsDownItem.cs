using UnityEngine;

public class PantsDownItem : GeneralItem
{
    public float multiplier;
    public HitscanGunSystem gun;

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void giveItem()
    {
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        Dashing dashing = player.GetComponent<Dashing>();

        // Gets player's currently equiped gun
        // TODO: add a check so player is not holding a melee weapon
        PlayerArsenalManager playerArsenal = player.GetComponent<PlayerArsenalManager>();
        HitscanGunSystem currentWeapon = playerArsenal.weapons[playerArsenal.currentWeaponIndex].GetComponentInChildren<HitscanGunSystem>();
        // GameObject.FindGameObjectsWithTag("Gun")

        PantsDownEffect buff = player.AddComponent<PantsDownEffect>();
        buff.movement = movement;
        buff.dashing = dashing;
        buff.gun = currentWeapon;
        buff.multiplier = multiplier;
        Destroy(gameObject);
    }


}
