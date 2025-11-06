using TMPro;
using UnityEngine;

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
    bool shooting, readyToShoot, reloading;

    [Header("References")]
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

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
            Reload();

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

        //RayCast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);

            // TODO: add damaging mechanics after adding enemy or health class
            // if (rayHit.collider.CompareTag("Enemy"))
            //     rayHit.collider.GetComponent<Enemy>().TakeDamage(damage);
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
    
}
