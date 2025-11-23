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
    int bulletsLeft, bulletsShot;

    // bools
    bool shooting, readyToShoot;
    public bool reloading;

    [Header("References")]
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    public LayerMask whatIsHitbox;

    [Header("Graphics")]
    public GameObject muzzleFlash, bulletHoleGraphic;
    // public CamShake camShake;
    // public float camShakeMagnitude, camShakeDuration;
    public TextMeshProUGUI bulletText;

    [Header("Input")]
    public KeyCode shootKey = KeyCode.Mouse0;
    public KeyCode reloadKey = KeyCode.R;

    protected virtual void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
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
            Reload();
        }

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }

    protected virtual void Shoot()
    {
        readyToShoot = false;

        // Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        Debug.Log(fpsCam.transform.forward);

        // Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + fpsCam.transform.right * x + fpsCam.transform.up * y;

        Ray ray = new Ray(fpsCam.transform.position, direction);
        
        if (Physics.Raycast(ray, out rayHit, range))
        {
            Debug.Log(rayHit.collider.name);

            if (((1 << rayHit.collider.gameObject.layer) & whatIsHitbox) != 0)
            {
                Enemy enemy = rayHit.collider.GetComponentInParent<Enemy>();

                if (enemy != null)
                {
                    if (rayHit.collider.CompareTag("Head"))
                        enemy.TakeDamage(damage * 2f);
                    else
                        enemy.TakeDamage(damage);
                }
            }
        }

        // Graphics
        Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 0, 0));

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    protected virtual void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}    
