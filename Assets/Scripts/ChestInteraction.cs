// TODO: change this script eventually that way its doesn't just handle chest interaction, handles all interactable objects

using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestInteraction : MonoBehaviour
{
    [Header("References")]
    public Camera fpsCam;
    public LayerMask whatIsInteractable;
    public TextMeshProUGUI interactText;

    [Header("Settings")]
    public float interactRange = 3f;
    public float sphereRadius = 0.3f;
    public KeyCode interactKey = KeyCode.F;

    [Header("UI")]
    public GameObject interactPromptUI;
    public TextMeshProUGUI promptText;

    private Interactable focusedItem;
    void Awake()
    {
        if (!fpsCam) fpsCam = Camera.main;
        if (interactPromptUI) interactPromptUI.SetActive(false);
    }

    void Update()
    {
        UpdateFocus();

        // Player pressed interact key
        if (focusedItem != null && Input.GetKeyDown(interactKey))
        {
            // if (CurrencyManager.current.money < focusedItem.chestCost)
            //     return;

            // CurrencyManager.current.AddMoney(-focusedItem.chestCost);

            focusedItem.Interact();
            ClearFocus();
        }
    }

    private void UpdateFocus()
    {
        Ray ray = new Ray(fpsCam.transform.position, fpsCam.transform.forward);
        RaycastHit hit;

        // SphereCast is just like Raycast but with a radius
        if (Physics.SphereCast(ray, sphereRadius, out hit, interactRange, whatIsInteractable, QueryTriggerInteraction.Collide))
        {
            // if (hit.collider.CompareTag("Chest"))
            // {
            //     Interactable chest = hit.collider.GetComponentInParent<Chest>();

            //     if (chest != null && !chest.isInteracted)
            //     {
            //         SetFocus(chest, hit);
            //         return;
            //     }
            // }
            Interactable item = hit.collider.GetComponentInParent<Interactable>();

            if (item != null && !item.isInteracted)
            {
                SetFocus(item, hit);
                return;
            }
        }

        ClearFocus();
    }

    private void SetFocus(Interactable item, RaycastHit hit)
    {
        focusedItem = item;

        if (interactPromptUI)
        {
            interactPromptUI.SetActive(true);

            // int cost = chest.chestCost;
            // int money = CurrencyManager.current.money;

            // promptText.text = money >= cost
            //     ? promptText.text = $"Press '{interactKey}' to open chest (Cost: {cost})"
            //     : promptText.text = $"Not enough money to open chest (Cost: {cost})";
            promptText.text = $"Press {interactKey} " + focusedItem.interactPrompt;
        }
    }
    
    private void ClearFocus()
    {
        if (focusedItem == null && !interactPromptUI.activeInHierarchy) return;
        focusedItem = null;

        if (interactPromptUI)
            interactPromptUI.SetActive(false);
    }
}
