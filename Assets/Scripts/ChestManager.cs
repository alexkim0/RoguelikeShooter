using UnityEngine;

public class ChestManager : MonoBehaviour
{
    public static int minCost = 20;
    public static int maxCost = 50;

    public float increaseInterval = 30f;
    public int minIncreaseAmount = 10;
    public int maxIncreaseAmount = 20;

    float nextIncreaseTime = 30f;

    void Update()
    {
        IncreaseChestCost();
    }

    public void IncreaseChestCost()
    {
        if (Time.time >= nextIncreaseTime)
        {
            minCost += minIncreaseAmount;
            maxCost += maxIncreaseAmount;

            nextIncreaseTime += increaseInterval;

            foreach (Chest chest in FindObjectsOfType<Chest>())
            {
                chest.UpdateChestCost();
                Debug.Log($"Chest range increased to {minCost} - {maxCost}");
            }
        }
    }
}   