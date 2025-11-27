using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;


public class PlayerStats : MonoBehaviour
{
    public float currentHealth;
    public float currentShield;
    public float maxShield = 100f;
    public float maxHealth = 100f;

    [Header("references")]
    public Image healthBar;
    public Image shideldBar;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI shieldText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        currentShield = maxShield;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0, 1);
        shideldBar.fillAmount = Mathf.Clamp(currentShield / maxShield, 0, 1);

        healthText.text = $"{currentHealth} / {maxHealth}";
        shieldText.text = $"{currentShield} / {maxShield}";
    }

    public void TakeDamage(float damageAmt)
    {
        if (currentShield > 0)
        {
            currentShield -= damageAmt;

            if (currentShield < 0)
            {
                currentShield = 0;
                currentHealth += currentShield; // currentShield is negative here
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

    public void IsDead()
    {
        if (currentHealth == 0)
        {
            SceneManager.LoadScene("Game Over");
        }
    }
}
