// TODO: might not need both patroling and sightRange..
// TODO: fix the playerMovement script that way it changes player velocity overtime and not instantly so the knockback works
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using DG.Tweening;

public class GroundedEnemyMovement : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public ParticleSystem spawnParticle;

    [Header("SpawnSettings")]
    private bool isSpawning;
    private Vector3 originalScale;
    public float spawnTime = 0.5f;

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
        originalScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    void Start()
    {
        if (isSpawning) return;
        StartCoroutine(SpawnRoutine());
    }

    protected virtual void Update()
    {
        if (isSpawning) return;
        // playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        // if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (!playerInAttackRange) ChasePlayer();
        else if (playerInAttackRange && !attacking && !onAttackCooldown ) AttackPlayer();

    }
    IEnumerator SpawnRoutine()
    {
        isSpawning = true;

        if (spawnParticle != null)
            spawnParticle.Play();

        yield return new WaitForSeconds(spawnParticle.main.duration);

        // tween scale from 0 → 1
        Tween t = transform.DOScale(originalScale, spawnTime).SetEase(Ease.OutBack);

        // wait until tween finishes
        yield return t.WaitForCompletion();

        isSpawning = false;

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

    protected virtual void AttackAudio()
    {
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
