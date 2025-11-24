using UnityEngine;
using System.Collections;

public class Rifle : HitscanGunSystem
{
    [Header("Rifle References")]
    public LineRenderer laserLineGenerator;
    public Transform laserOrigin;
    public float laserDuration = 0.5f;

    protected override void Shoot()
    {

        laserLineGenerator.SetPosition(0, laserOrigin.position);
        base.Shoot();

        if (rayHit.collider)
        {
            laserLineGenerator.SetPosition(1, rayHit.point);
        } else
        {
            laserLineGenerator.SetPosition(1, laserOrigin.position + (fpsCam.transform.forward * range));
        }
        StartCoroutine(ShootLaser());
    }

    IEnumerator ShootLaser()
    {
        laserLineGenerator.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLineGenerator.enabled = false;
    }
}
