// TODO: might not need both patroling and sightRange..

using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class GroundedEnemyMovement : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent agent;
    public Rigidbody rb;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public Animator animator;

    [Header("DashSetting")]
    public float dashSpeed = 18f;
    public float dashDuration = 0.25f;
    public float dashCooldown = 1.5f;

    [Header("Patroling")]
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    [Header("Attacking")]
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    [Header("States")]
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private Vector3 dashDirection;
    private bool dashing, onDashCooldown;

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    void Update()
    {
        // playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        // if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (!playerInAttackRange) ChasePlayer();
        else if (playerInAttackRange && !dashing && !onDashCooldown ) AttackPlayer();

        float currentSpeed = agent.velocity.magnitude;
    
        // Convert current movement speed to a usable multiplier
        float multiplier = Mathf.Clamp(currentSpeed / agent.speed, 0.5f, 2f);
        animator.SetFloat("RunningMultiplier", multiplier);
    }

    private void Patroling()
    {
        
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        dashDirection = player.position - transform.position; dashDirection.y = 0; dashDirection = dashDirection.normalized;
        animator.Play("EnemyCharging", 0, 0f);
        StartCoroutine(Dash(dashDirection));
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    IEnumerator Dash(Vector3 dashDir)
    {
        dashing = true; onDashCooldown = true;

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
            rb.MovePosition(rb.position + (dashDir * dashSpeed * Time.fixedDeltaTime));
            t += Time.fixedDeltaTime;
            yield return waitFixed;
        }

        // Hand control back to agent (sync positions)
        agent.Warp(rb.position);
        agent.updatePosition = true;
        agent.updateRotation = true;
        agent.isStopped = wasStopped;

        dashing = false;
        yield return new WaitForSeconds(dashCooldown);
        onDashCooldown = false;
    }






}
