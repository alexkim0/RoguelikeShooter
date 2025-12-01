using System;
using UnityEngine;

public class Boss : Enemy
{
    public string bossName = "Rage Hand";

    // changes health ui
    public Action<float, float> onHealthChanged; // (current, Max)
    // enables boss ui
    public Action onBossFightStarted;
    // disables boss ui
    public Action onBossDied;   

    protected bool fightStarted;

    void Awake()
    {
        maxHealth = 1000;
        currentHealth = maxHealth;
    }

    public void StartFight()
    {
        if (fightStarted) return;
        fightStarted = true;
        onBossFightStarted?.Invoke();
        // enable AI, music, etc
    }

    public override void TakeDamage(float amount)
    {
        if (!fightStarted) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        onHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0f)
        {
            Death();
        }
    }

    protected override void Death()
    {
        onBossDied?.Invoke();
        // play death anim, disable AI, drop loot, etc.
        Destroy(gameObject, 3f);
    }

}
