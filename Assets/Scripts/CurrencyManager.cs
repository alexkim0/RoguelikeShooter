using UnityEngine;
using System;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager current;
    public int money;
    public Action onMoneyChanged;

    void Awake()
    {
        current = this;
    }

    void Update()
    {
        
    }

    public void AddMoney(int amount)
    {
        money += amount;
        onMoneyChanged?.Invoke();
        //Debug.Log("Money: " + money);
    }

}