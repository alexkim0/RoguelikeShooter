using UnityEngine.UI;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100f;

    [Header("references")]
    public Image healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0, 1);
    }

    public void TakeDamage(float damageAmt)
    {
        currentHealth -= damageAmt;
    }
}
