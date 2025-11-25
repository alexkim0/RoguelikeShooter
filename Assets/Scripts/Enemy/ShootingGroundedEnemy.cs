using UnityEngine;

public class ShootingGroundedEnemy : GroundedEnemyMovement
{
    [Header("Shooting References")]
    public GameObject projectile;
    public Transform head;

    [Header("Attacking")]
    public float timeBetweenAttacks = 0.5f;
    protected bool alreadyAttacked = false;
    protected override void AttackPlayer()
    {
        Shoot();
    }

    private void Shoot()
    {
        agent.SetDestination(transform.position);

        head.LookAt(player);

        if (!alreadyAttacked)
        {
            Rigidbody projectileRb = Instantiate(projectile, head.position, Quaternion.identity).GetComponent<Rigidbody>();
            projectileRb.AddForce(head.forward * 32f, ForceMode.Impulse);
            projectileRb.AddForce(head.forward * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
