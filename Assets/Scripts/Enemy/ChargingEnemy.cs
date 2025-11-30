using UnityEngine;
using System.Collections;
using DG.Tweening;
using NUnit.Framework;

public class ChargingEnemy : GroundedEnemyMovement
{
    [Header("References")]
    public Animator animator;
    public Rigidbody rb;
    // public ParticleSystem spawnParticle;

    // [Header("SpawnSettings")]
    // private bool isSpawning;
    // private Vector3 originalScale;
    // public float spawnTime = 0.5f;
    


    [Header("DashSetting")]
    public float dashSpeed = 30f;
    public float dashDuration = 0.25f;
    public float dashCooldown = 1f;
    public float dashDamage = 20f;

    private Vector3 dashDirection;
    // checking if enemy did damage during dash
    private bool didDamage = false;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        // originalScale = transform.localScale;
        // transform.localScale = Vector3.zero;
    }

    // void Start()
    // {
    //     if (isSpawning) return;
    //     StartCoroutine(SpawnRoutine());
    // }

    protected override void Update()
    {
        // if (isSpawning) return;
        base.Update();
        float currentSpeed = agent.velocity.magnitude;
    
        // Convert current movement speed to a usable multiplier
        float multiplier = Mathf.Clamp(currentSpeed / agent.speed, 0.5f, 2f);
        animator.SetFloat("RunningMultiplier", multiplier);
    }

    protected override void AttackPlayer()
    {
        dashDirection = player.position - transform.position; dashDirection.y = 0; dashDirection = dashDirection.normalized;
        animator.Play("EnemyCharging", 0, 0f);
        StartCoroutine(Dash(dashDirection));
    }

    // IEnumerator SpawnRoutine()
    // {
    //     isSpawning = true;

    //     if (spawnParticle != null)
    //         spawnParticle.Play();

    //     yield return new WaitForSeconds(spawnParticle.main.duration);

    //     // tween scale from 0 → 1
    //     Tween t = transform.DOScale(originalScale, spawnTime).SetEase(Ease.OutBack);

    //     // wait until tween finishes
    //     yield return t.WaitForCompletion();

    //     isSpawning = false;


    // }

    IEnumerator Dash(Vector3 dashDir)
    {
        attacking = true; onAttackCooldown = true;

        // Pause agent and take over motion
        bool wasStopped = agent.isStopped;
        agent.isStopped = true;
        agent.updatePosition = false;
        agent.updateRotation = false;

        transform.rotation = Quaternion.LookRotation(dashDir);

        float t = 0f;
        // WaitForFixedUpdate: instance that you can "yield return" in coroutine to wait until the next FixedUpdate step
        WaitForFixedUpdate waitFixed = new WaitForFixedUpdate();
        
        while (t < dashDuration)
        {
            // 1) Gets position where the enemy will go next fixedframe
            Vector3 nextPos = rb.position + dashDir * dashSpeed * Time.fixedDeltaTime;

            // 2) Raycast down to find ground below that point
            // QueryTriggerIneraction.Ignore : ignores collision with triggers 
            if (Physics.Raycast(nextPos + Vector3.up * groundCheckHeight,
                                Vector3.down,
                                out RaycastHit hit,
                                groundCheckHeight * 3f,
                                groundMask,
                                QueryTriggerInteraction.Ignore))
            {
                // Snap to ground (plus a tiny offset)
                nextPos.y = hit.point.y + groundOffset;
            }

            // 3) Apply the move
            rb.MovePosition(nextPos);

            // 4) increase time
            t += Time.fixedDeltaTime;
            yield return waitFixed;
        }

        // Hand control back to agent (sync positions)
        agent.Warp(rb.position);
        agent.updatePosition = true;
        agent.updateRotation = true;
        agent.isStopped = wasStopped;

        attacking = false;
        yield return new WaitForSeconds(dashCooldown);
        onAttackCooldown = false;
        didDamage = false;
    }
    void OnCollisionEnter(Collision other)
    {
        if (attacking && other.gameObject.CompareTag("Player") && !didDamage)
        {
            PlayerStats stat = player.GetComponent<PlayerStats>();
            stat.TakeDamage(dashDamage);

            // player.GetComponent<Rigidbody>().AddForce(dashDirection * 100f, ForceMode.Impulse);
            didDamage = true;
        }
    }
}
