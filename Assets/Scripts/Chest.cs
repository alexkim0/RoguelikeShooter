
using TMPro;
using UnityEngine;

public class Chest : Interactable
{
    [Header("References")]
    public Animator animator; // optional: chest opening animation
    public GameObject[] itemPool = new GameObject[2];
    public ChestEnemySpawner enemySpawner;
    public TextMeshPro chestCostText;

    [Header("Audio")]
    public AudioSource chestOpenAudio;
    public AudioClip chestOpenClip;

    [Header("Chest Cost")]
    public int chestCost;

    [Header("Player Detection")]
    public float checkRange = 10f;
    public LayerMask whatIsPlayer;
    void Awake()
    {
        interactPrompt = "to open this chest";
    }

    void Start()
    {
        chestCost = Random.Range(ChestManager.minCost, ChestManager.maxCost + 1);
        enemySpawner = GetComponentInChildren<ChestEnemySpawner>();
        Debug.Log($"Chest spawned with cost {chestCost}");
        chestCostText.text = $"${chestCost}";
        chestCostText.enabled = false;
    }

    void Update()
    {
        ShowCost();
    }

    void Reset() {
        // simple convenience to auto-assign an Animator if present
        animator = GetComponent<Animator>();
    }

    public override void Interact()
    {
        if (isInteracted) return;

        // Checks if players has more money than the chest value
        if (CurrencyManager.current.money < chestCost)
            return;

        isInteracted = true;

        // takes off money
        CurrencyManager.current.AddMoney(-chestCost);

        enemySpawner.SpawnEnemies();

        if (animator != null) animator.enabled = true;

        // any other logic: grant loot, play sfx, disable collider, etc.
        if (chestOpenAudio != null)
        {
            chestOpenAudio.PlayOneShot(chestOpenClip);
        }

        chestCostText.enabled = false;
    }

    public void SpawnItem()
    {
        int index = Random.Range(0, itemPool.Length);
        Debug.Log(index);
        Instantiate(itemPool[index], transform.position, Quaternion.identity);
    }

    public void UpdateChestCost()
    {
        chestCost = Random.Range(ChestManager.minCost, ChestManager.maxCost + 1);
        Debug.Log($"Chest cost updated to {chestCost}");
    }

    private void ShowCost()
    {
        if (isInteracted) return;

        bool isPlayerInRange = Physics.CheckSphere(transform.position, checkRange, whatIsPlayer);

        if (isPlayerInRange)
        {
            chestCostText.enabled = true;
        } else
        {
            chestCostText.enabled = false;
        }
    }
}
