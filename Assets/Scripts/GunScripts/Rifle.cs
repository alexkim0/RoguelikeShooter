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

    [Header("Audio")]
    public AudioSource rifleAudio; // https://pixabay.com/sound-effects/laser-104024/
    public AudioClip rifleClip; //https://pixabay.com/sound-effects/laser-104024/
    public AudioSource reloadAudio; // https://pixabay.com/sound-effects/1911-reload-6248/

    protected override void Awake()
    {
        // rifle preset
        damage = 10;
        timeBetweenShooting = 0.1f;
        spread = 0.01f;
        range = 100;
        reloadTime = 1.5f;  
        magazineSize = 30;
        bulletsPerTap = 0;
        allowButtonHold = true;
        base.Awake();

    }

    protected override void Reload()
    {
        reloadAudio.Play();
        base.Reload();
    }

    protected override void Shoot()
    {
        currentLineTrail = Instantiate(laserTrail).GetComponent<LineRenderer>();

        PlayShootAnimation();

        currentLineTrail.SetPosition(0, laserOrigin.position);
        rifleAudio.PlayOneShot(rifleClip, 1.0f);
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
