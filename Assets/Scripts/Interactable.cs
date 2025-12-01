using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Interactable Settings")]
    public bool isInteracted = false;
    public string interactPrompt = "Press F to interact";

    public virtual void Interact()
    {
        
    }
}
