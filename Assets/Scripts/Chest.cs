
using UnityEngine;

public class Chest : MonoBehaviour
{
    public bool isOpen = false;
    public Animator animator; // optional: chest opening animation
    public GameObject lootPrefab; // optional: spawn loot
    public GameObject[] itemPool = new GameObject[2];


    void Reset() {
        // simple convenience to auto-assign an Animator if present
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        if (isOpen) return;
        isOpen = true;

        if (animator != null) animator.enabled = true;
        int index = Random.Range(0, itemPool.Length);
        Debug.Log(index);
        if (lootPrefab != null) Instantiate(itemPool[index], transform.position + Vector3.up * 1.5f, Quaternion.identity);

        // any other logic: grant loot, play sfx, disable collider, etc.


    }
}
