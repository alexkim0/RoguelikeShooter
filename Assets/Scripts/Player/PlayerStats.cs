using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using System;


public class PlayerStats : MonoBehaviour
{
    public float currentHealth;
    public float currentShield;
    public float maxShield = 100f;
    public float maxHealth = 100f;

    [Header("Shield Regen")]
    public float shieldRegenDelay = 5f;
    public float shieldRegenRate = 10f;

    // Tracks time at which the player last took damage
    private float lastDamageTime = -Mathf.Infinity;

    [Header("references")]
    public Slider healthBar;
    public Image shideldBar;
    public TextMeshProUGUI healthText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        currentShield = maxShield;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = Mathf.Clamp(currentHealth / maxHealth, 0, 1);
        shideldBar.fillAmount = Mathf.Clamp(currentShield / maxShield, 0, 1);

        healthText.text = $"{currentHealth}/{maxHealth}";
        
        RegenerateShield();
    }

    public void TakeDamage(float damageAmt)
    {
        // Mark the time the player was hit so regen can be delayed
        lastDamageTime = Time.time;

        if (currentShield > 0)
        {
            currentShield -= damageAmt;

            // If the damage exceeded our shield, apply the leftover damage to health
            if (currentShield < 0)
            {
                float leftoverdamage = -currentShield; // Leftover damage after shield depletes
                currentShield = 0;
                currentHealth -= leftoverdamage;
                if (currentHealth < 0) 
                {
                    currentHealth = 0;
                }
            }
        }
        else
        {
            currentHealth -= damageAmt;

            if (currentHealth < 0)
            {
                currentHealth = 0;
            }
        }   

        //IsDead();
    }

    public void RegenerateShield()
    {
        // Only regen if player is still alive and shield is not full
        if (currentHealth <= 0) return;
        if (currentShield >= maxShield) return;

        // If the required delay hasn't passed yet, do nothing
        if (Time.time - lastDamageTime < shieldRegenDelay) return;

        // Regenerate shield over time
        currentShield += shieldRegenRate * Time.deltaTime;
        
        if (currentShield > maxShield) currentShield = maxShield;
    }


    public void IsDead()
    {
        if (currentHealth == 0)
        {
            SceneManager.LoadScene("Game Over");
        }
    }
}
