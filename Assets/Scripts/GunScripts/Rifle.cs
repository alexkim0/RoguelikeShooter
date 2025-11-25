using UnityEngine;
using System.Collections;

public class Rifle : HitscanGunSystem
{
    [Header("Rifle References")]
    public GameObject laserTrail;
    public Transform laserOrigin;
    public float laserDuration = 0.5f;
    public Animator animator;
    private LineRenderer currentLineTrail;

    protected override void Shoot()
    {
        currentLineTrail = Instantiate(laserTrail).GetComponent<LineRenderer>();

        PlayShootAnimation();

        currentLineTrail.SetPosition(0, laserOrigin.position);
        base.Shoot();

        if (rayHit.collider)
        {
            currentLineTrail.SetPosition(1, rayHit.point);
        } else
        {
            currentLineTrail.SetPosition(1, laserOrigin.position + (fpsCam.transform.forward * range));
        }
    }

    IEnumerator ShootLaser()
    {
        currentLineTrail.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        currentLineTrail.enabled = false;
    }

    private void PlayShootAnimation()
    {
        animator.Play("RifleShoot", 0, 0f);

    }
}
