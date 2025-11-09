using UnityEngine;

public class Chest : MonoBehaviour
{
    public bool isOpen = false;
    public Animator animator; // optional: chest opening animation
    public GameObject lootPrefab; // optional: spawn loot

    void Reset() {
        // simple convenience to auto-assign an Animator if present
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        if (isOpen) return;
        isOpen = true;

        if (animator != null) animator.enabled = true;
        // if (lootPrefab != null) Instantiate(lootPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);

        // any other logic: grant loot, play sfx, disable collider, etc.
    }
}
