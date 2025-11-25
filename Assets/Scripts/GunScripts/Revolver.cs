using UnityEngine;
using System.Collections;

public class Revolver : HitscanGunSystem
{
    [Header("Revolver References")]
    public Animator anim;
    public LineRenderer laserLine;
    public Transform laserOrigin;
    public float laserDuration = 0.5f;

    [Header("Audio")]
    public AudioSource rifleAudio;
    public AudioClip rifleClip;
    public AudioSource reloadAudio;

    protected override void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
        // revolver preset
        damage = 30;
        timeBetweenShooting = 0.6f;
        spread = 0.02f;
        range = 100;
        reloadTime = 1f;
        magazineSize = 6;
        bulletsPerTap = 1;
        allowButtonHold = false;
        base.Awake();

    }

    protected override void Reload()
    {
        reloadAudio.Play();
        base.Reload();
    }

    protected override void Shoot()
    {
        PlayShootAnimation();

        laserLine.SetPosition(0, laserOrigin.position);
        rifleAudio.PlayOneShot(rifleClip, 1.0f);
        base.Shoot();

        if (rayHit.collider)
        {
            laserLine.SetPosition(1, rayHit.point);
        } else
        {
            laserLine.SetPosition(1, laserOrigin.position + (fpsCam.transform.forward * range));
        }
        StartCoroutine(ShootLaser());
    }

    private void PlayShootAnimation()
    {
        anim.Play("RevolverShoot", 0, 0f);

    }

    IEnumerator ShootLaser()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }
}
