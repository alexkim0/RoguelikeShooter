using UnityEngine;
using DG.Tweening;
using System.Collections;

public class GeneralItem : Interactable
{
    [Header("References")]
    public GameObject player;

    [Header("Animation Settings")]
    // how high the items float
    public float floatStrength = 0.25f;
    // how fast the item goes up and down
    public float floatSpeed = 2f;
    public float rotateSpeed = 50f;
    private float startY;

    [Header("Spawn Animation Settings")]
    public float spawnTime = 0.5f;
    public float spawnY = 1f;
    private Vector3 originalScale;
    private bool isSpawning;
    // float for subtracting from Time.time so the floating process starts at spawnedY
    private float floatStartTime;

    void Awake()
    {
        interactPrompt = "to get this item";
        player = GameObject.FindGameObjectWithTag("Player");
        originalScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    protected virtual void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    void Update()
    {
        if (!isSpawning)
        {
            FloatAndRotate();
        }
    }

    IEnumerator SpawnRoutine()
    {
        isSpawning = true;

        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + Vector3.up * 1f;

        Sequence seq = DOTween.Sequence();

        seq.Join(transform.DOScale(originalScale, spawnTime).SetEase(Ease.OutBack));
        seq.Join(transform.DOMove(endPos, spawnTime).SetEase(Ease.OutCubic));

        // wait until tween finishes
        yield return seq.WaitForCompletion();

        startY = endPos.y;

        // reset the float phase so Sin starts at 0
        floatStartTime = Time.time;

        isSpawning = false;
    }

    private void FloatAndRotate()
    {
        float t = Time.time - floatStartTime;
        float newY = startY + Mathf.Sin(t * floatSpeed) * floatStrength;

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0); // slow spin
    }

    public virtual void giveItem()
	{
		
	}

    public override void Interact()
    {
        giveItem();
    }

    

    
}
