using UnityEngine;

public class ShootingGroundedEnemy : GroundedEnemyMovement
{
    [Header("Shooting References")]
    public GameObject projectile;

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

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Rigidbody projectileRb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            projectileRb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            projectileRb.AddForce(transform.forward * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
