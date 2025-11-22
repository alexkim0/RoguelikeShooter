using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class ShootingProjectile : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            PlayerStats stat = other.gameObject.GetComponent<PlayerStats>();
            if (stat != null)
            {
                stat.TakeDamage(20f);
            }
            
            Destroy(gameObject);
        }
        else if (!other.gameObject.CompareTag("Enemy")) Destroy(gameObject);
    }
}
