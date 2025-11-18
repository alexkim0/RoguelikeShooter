using UnityEngine;

public class FireDamage : MonoBehaviour
{
    public float damage=6;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemyHealth = other.GetComponent<Enemy>();
            if (enemyHealth != null)
                enemyHealth.TakeDamage(damage);
        }
    }
}
