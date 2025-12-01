using UnityEngine;
using System.Collections;

public class HandBoss : Boss
{
    [Header("References")]
    public GameObject player;
    public Transform origin;
    public Animator anim;

    [Header("Slam Settings")]
    public float handHeight = 30f;
    public float followSpeed = 1f;
    private bool isSlamming;
    public float slamSpeed = 10f;
    public float slamCooldown = 5f;
    public float slamCooldownTimer;
    public float resetSpeed = 30f;
    public bool isResetting;
    public float rotateSpeed = 5f;

    [Header("Ground Detection")]
    public LayerMask whatIsGround;
    public float raycastHeight = 60f;
    public float groundOffset = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!fightStarted) return;
        
        if (!isSlamming && !isResetting)
            StareAtPlayer();
        if (!isSlamming && !isResetting)
            FollowPlayer();
    }

    private void StareAtPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0f; // remove vertical component -> prevents looking up/down

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        origin.rotation = Quaternion.RotateTowards(
            origin.rotation,
            targetRotation,
            rotateSpeed * Time.deltaTime
        );
    }
    private void FollowPlayer()
    {
        Vector3 targetPos = player.transform.position;

        // Move enemy toward player every frame
        transform.position = Vector3.MoveTowards(
            transform.position,       // current position
            new Vector3(targetPos.x - 5, handHeight, targetPos.z),                // destination
            followSpeed * Time.deltaTime // movement speed
        );

        Slam();
    }

    private void Slam()
    {
        if (!isSlamming && slamCooldownTimer < 0 && !isResetting)
            StartCoroutine(SlamRoutine());
        else
            slamCooldownTimer -= Time.deltaTime;
    }
    private IEnumerator SlamRoutine()
    {
        isSlamming = true;

        anim.SetTrigger("Slam");

        float t = 0.25f;

        while (t > 0)
        {
            t -= Time.deltaTime;
            yield return null;
        }

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, raycastHeight, whatIsGround))
        {
            Vector3 targetPos = hit.point;

            targetPos += Vector3.up * groundOffset;

            while (Vector3.Distance(transform.position, targetPos) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    targetPos,
                    slamSpeed * Time.deltaTime
                );

                yield return null;
            }
        }

        isSlamming = false;
        
        slamCooldownTimer = slamCooldown;
        StartCoroutine(ResetRoutine());
    }

    private IEnumerator ResetRoutine()
    {
        isResetting = true;
        Vector3 targetPos = transform.position + Vector3.up * handHeight;

        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                resetSpeed * Time.deltaTime
            );

            yield return null;
        }
        isResetting = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isSlamming && collision.gameObject.CompareTag("Player"))
        {
            PlayerStats playerStats = player.GetComponent<PlayerStats>();
            playerStats.TakeDamage(40f);
        }
    }
}
