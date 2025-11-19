using UnityEngine;

public class PantsDownEffect : MonoBehaviour
{
    public HitscanGunSystem gun;
    public PlayerMovement movement;
    public Dashing dashing;
    public float multiplier;

    private bool buffActive = false;

    void Update()
    {
        // When reload starts → apply buff once
        if (gun.reloading && !buffActive)
        {
            ApplyBuff();
        }
        // When reload ends → remove buff once
        else if (!gun.reloading && buffActive)
        {
            RemoveBuff();
        }
    }

    void ApplyBuff()
    {
        buffActive = true;

        movement.walkSpeed             *= multiplier;
        movement.maxAirSpeed           *= multiplier;
        movement.downwardSlideSpeed    *= multiplier;
        movement.dashSpeedChangeFactor *= multiplier;
        movement.dashSpeed             *= multiplier;
        movement.slideSpeed            *= multiplier;
        dashing.dashForce              *= multiplier;
    }

    void RemoveBuff()
    {
        buffActive = false;

        movement.walkSpeed             /= multiplier;
        movement.maxAirSpeed           /= multiplier;
        movement.downwardSlideSpeed    /= multiplier;
        movement.dashSpeedChangeFactor /= multiplier;
        movement.dashSpeed             /= multiplier;
        movement.slideSpeed            /= multiplier;
        dashing.dashForce              /= multiplier;
    }
}
