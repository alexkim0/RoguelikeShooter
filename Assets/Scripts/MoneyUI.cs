using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;

    void Start()
    {
        IncreaseMoneyText();
        CurrencyManager.current.onMoneyChanged += IncreaseMoneyText;
    }

    void IncreaseMoneyText()
    {
        moneyText.text = "Money: " + CurrencyManager.current.money;
    }

}