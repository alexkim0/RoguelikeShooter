using UnityEngine;

public class FireDamage : MonoBehaviour
{
    public float damage = 6;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.CompareTag("Body"))
        {
            Enemy enemyHealth = other.GetComponentInParent<Enemy>();
            if (enemyHealth != null)
                enemyHealth.TakeDamage(damage);
        }
    }
}
