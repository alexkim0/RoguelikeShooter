using UnityEngine;
using DG.Tweening;
using System.Collections;

public class FlyingEnemyMovement : MonoBehaviour
{
    [Header("References")]
    public GameObject player;
    public ParticleSystem spawnParticle;
    public ParticleSystem attackParticle;
    public float speed = 3f;
    [Header("Spawn Settings")]
    public float spawnTime = 0.5f;
    private Vector3 originalScale;
    private bool isSpawning;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Awake()
    {
        originalScale = transform.localScale;
        transform.localScale = Vector3.zero;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        StartCoroutine(SpawnRoutine());

    }

    IEnumerator SpawnRoutine()
    {
        isSpawning = true;

        if (spawnParticle != null)
            spawnParticle.Play();

        yield return new WaitForSeconds(spawnParticle.main.duration);

        // tween scale from 0 → 1
        Tween t = transform.DOScale(originalScale, spawnTime).SetEase(Ease.OutBack);

        // wait until tween finishes
        yield return t.WaitForCompletion();

        isSpawning = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawning) return;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        transform.LookAt(player.transform.position);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats stat = player.GetComponent<PlayerStats>();

            stat.TakeDamage(40f);

            Instantiate(attackParticle, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
