using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;


public class PlayerStats : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100f;

    [Header("references")]
    public Image healthBar;
    public TextMeshProUGUI healthText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0, 1);
        healthText.text = $"{currentHealth} / {maxHealth}";
    }

    public void TakeDamage(float damageAmt)
    {
        currentHealth -= damageAmt;
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
