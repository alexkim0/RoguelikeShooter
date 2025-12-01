using UnityEngine;
using DG.Tweening;
using System.Collections;

public class BossBeacon : Interactable
{
    public GameObject bossPrefab;
    public BossUI bossUI;

    [Header("Camera Shake Settings")]
    public float duration;
    public float strength;
    void Awake()
    {
        interactPrompt = "to start the Challenge";
        bossUI = UIManager.instance.bossUI;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {   
        if (isInteracted) return;
        StartCoroutine(BossSpawnRoutine());
    }

    IEnumerator BossSpawnRoutine()
    {
        Tween t = Camera.main.transform.DOShakePosition(duration, strength, 20, 90, false, true);

        yield return t.WaitForCompletion();

        Vector3 spawnPos = transform.position + Vector3.up * 50f;
        Boss boss = Instantiate(bossPrefab, spawnPos, Quaternion.identity).GetComponent<Boss>();

        // Bind the UI to this boss and start the fight
        bossUI.BindBoss(boss);
        boss.StartFight();

        isInteracted = true;
    }


}
