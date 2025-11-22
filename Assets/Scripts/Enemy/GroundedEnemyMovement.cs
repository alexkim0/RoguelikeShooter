// TODO: might not need both patroling and sightRange..
// TODO: fix the playerMovement script that way it changes player velocity overtime and not instantly so the knockback works
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class GroundedEnemyMovement : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    [Header("AttackSetting")]
    public float attackDuration = 0.25f;
    public float attackCooldown = 1.5f;

    [Header("Patroling")]
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    [Header("States")]
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    // For slopes
    [Header("GroundCheck")]
    public LayerMask groundMask;
    public float groundCheckHeight = 1.0f;   // from above
    public float groundOffset = 1f;        // small offset above ground

    public bool attacking, onAttackCooldown;
    // checking if enemy did damage during dash

    protected virtual void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        // playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        // if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (!playerInAttackRange) ChasePlayer();
        else if (playerInAttackRange && !attacking && !onAttackCooldown ) AttackPlayer();

    }

    private void Patroling()
    {
        
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    protected virtual void AttackPlayer()
    {
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
