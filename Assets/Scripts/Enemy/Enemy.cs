using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    public int moneyDropped;
    public float currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float damageAmt)
    {
        currentHealth -= damageAmt;
        DamagePopUpGenerator.currentGenerator.CreateDamagePopUp(transform.position, damageAmt.ToString());
        if (currentHealth <= 0f)
        {
            Death();
        }
    }

    protected virtual void Death()
    {
        CurrencyManager.current.AddMoney(moneyDropped);
        Destroy(gameObject);
    }
}
