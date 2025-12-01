// Just stores the reference of ui so it is fast to search

using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public BossUI bossUI;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
}
