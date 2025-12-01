using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;

public class BossBeacon : Interactable
{
    public GameObject bossPrefab;
    public BossUI bossUI;

    [Header("Camera Shake Settings")]
    public float duration;
    public float strength;

    [Header("References")]
    public Material completeMaterial;
    public MeshRenderer mr;
    private bool defeated = false;
    void Awake()
    {
        interactPrompt = "to start the Challenge";
        bossUI = UIManager.instance.bossUI;
        mr = GetComponentInChildren<MeshRenderer>();
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
        if (!isInteracted && defeated)
            SceneManager.LoadScene("End");
        else if (!isInteracted)
            StartCoroutine(BossSpawnRoutine());

    }

    IEnumerator BossSpawnRoutine()
    {
        Tween t = Camera.main.transform.DOShakePosition(duration, strength, 20, 90, false, true);

        yield return t.WaitForCompletion();

        Vector3 spawnPos = transform.position + Vector3.up * 50f;
        Boss boss = Instantiate(bossPrefab, spawnPos, Quaternion.identity).GetComponent<Boss>();
        boss.onBossDied += Completed;

        // Bind the UI to this boss and start the fight
        bossUI.BindBoss(boss);
        boss.StartFight();

        isInteracted = true;
    }

    private void Completed()
    {
        defeated = true;
        isInteracted = false;
        interactPrompt = "to leave";
        Material[] mats = mr.materials;
        mats[0] = completeMaterial;
        mr.materials = mats;
    }


}
