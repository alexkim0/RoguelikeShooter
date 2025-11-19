using UnityEngine.UI;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float health;
    public float maxHealth;

    [Header("references")]
    public Image healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
    }
}
