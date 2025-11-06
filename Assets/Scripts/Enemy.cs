using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
    }

    public void TakeDamage(float damageAmt)
    {
        currentHealth -= damageAmt;
    }
    
    private void CheckHealth()
    {
        if (currentHealth < 0)
        {
            Destroy(gameObject);
        }
    }
}
