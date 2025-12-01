using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossUI : MonoBehaviour
{
    public GameObject bossUIRoot;
    public Image healthSlider;
    public TextMeshProUGUI bossNameText;

    private Boss boss;

    void Awake()
    {
        bossUIRoot.SetActive(false);
    }

    public void BindBoss(Boss newBoss)
    {
        if (boss != null)
        {
            // unsubscribe from old boss
            boss.onHealthChanged -= OnHealthChanged;
            boss.onBossFightStarted -= OnBossFightStarted;
            boss.onBossDied -= OnBossDied;
        }

        boss = newBoss;

        if (boss == null) return;

        boss.onHealthChanged += OnHealthChanged;
        boss.onBossFightStarted += OnBossFightStarted;
        boss.onBossDied += OnBossDied;

        bossNameText.text = boss.bossName;
        // healthSlider.maxValue = boss.maxHealth;
        // healthSlider.value = boss.currentHealth;
        healthSlider.fillAmount = 1f;
    }

    void OnBossFightStarted()
    {
        bossUIRoot.SetActive(true);
    }


    void OnBossDied()
    {
        bossUIRoot.SetActive(false);
    }


    void OnHealthChanged(float current, float max)
    {
        // healthSlider.maxValue = max;
        // healthSlider.value = current;
        healthSlider.fillAmount = Mathf.Clamp(current / max, 0, 1);
    }

}
