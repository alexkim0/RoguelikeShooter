using TMPro;
using UnityEngine;
using System.Collections;

public class HitscanGunSystem : MonoBehaviour
{
    [Header("Gun Stats")]
    public int damage;
    // timeBetweenShots: if 0, then it will act like shotgun, if > 0, burst
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    // decides whether your weapon will be a hold weapon or tap weapon
    public bool allowButtonHold;
    public float laserDuration = 0.5f;
    int bulletsLeft, bulletsShot;

    // bools
    bool shooting, readyToShoot, reloading;

    [Header("Audio")]
    public AudioSource rifleAudio;
    public AudioSource reloadAudio;

    [Header("References")]
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    public LayerMask whatIsHitbox;
    public Animator anim;
    public LineRenderer laserLine;
    public Transform laserOrigin;

    [Header("Graphics")]
    public GameObject muzzleFlash, bulletHoleGraphic;
    // public CamShake camShake;
    // public float camShakeMagnitude, camShakeDuration;
    public TextMeshProUGUI bulletText;

    [Header("Input")]
    public KeyCode shootKey = KeyCode.Mouse0;
    public KeyCode reloadKey = KeyCode.R;

    void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        laserLine = GetComponent<LineRenderer>();
    }

    void Update()
    {
        MyInput();

        // SetText
        bulletText.SetText(bulletsLeft + " / " + magazineSize);
    }

    private void MyInput()
    {
        // shooting == input is pressed
        if (allowButtonHold)
            shooting = Input.GetKey(shootKey);
        else
            shooting = Input.GetKeyDown(shootKey);

        // TODO: might discard this if we don't want a reload mechanism
        if (Input.GetKeyDown(reloadKey) && bulletsLeft < magazineSize && !reloading)
        {
            reloadAudio.Play();
            Reload();
        }

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        // Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        // Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);
        laserLine.SetPosition(0, laserOrigin.position);

        Ray ray = new Ray(fpsCam.transform.position, direction);

        PlayShootAnimation();

        if (Physics.Raycast(ray, out rayHit, range, whatIsHitbox))
        {
            Debug.Log(rayHit.collider.name);
            Enemy enemy = rayHit.collider.GetComponentInParent<Enemy>();
            laserLine.SetPosition(1, rayHit.point);

            if (enemy != null)
            {
                if (rayHit.collider.CompareTag("Head"))
                    enemy.TakeDamage(damage * 2f);
                else
                    enemy.TakeDamage(damage);

            }
        }
        // 1️⃣ Broad raycast for visuals (any object)
        else if (Physics.Raycast(ray, out rayHit, range))
        {
            // Always spawn bullet hole
            Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.LookRotation(rayHit.normal));
            laserLine.SetPosition(1, rayHit.point);
        }
        //RayCast
        else
        {
            laserLine.SetPosition(1, laserOrigin.position + (fpsCam.transform.forward * range));
        }
        StartCoroutine(ShootLaser());

        // Graphics
        Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 0, 0));

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }

    IEnumerator ShootLaser()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

    private void PlayShootAnimation()
    {
        anim.Play("RevolverShoot", 0, 0f);

    }
}    
