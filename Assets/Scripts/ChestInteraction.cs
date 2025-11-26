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

    private Chest focusedChest;
    void Awake()
    {
        if (!fpsCam) fpsCam = Camera.main;
        if (interactPromptUI) interactPromptUI.SetActive(false);
    }

    void Update()
    {
        UpdateFocus();

        // Player pressed interact key
        if (focusedChest != null && Input.GetKeyDown(interactKey))
        {
            focusedChest.Open();
            ClearFocus();
        }
    }

    private void UpdateFocus()
    {
        Ray ray = new Ray(fpsCam.transform.position, fpsCam.transform.forward);
        RaycastHit hit;

        // SphereCast is just like Raycast but with a radius
        if (Physics.SphereCast(ray, sphereRadius, out hit, interactRange, whatIsInteractable, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.CompareTag("Chest"))
            {
                Chest chest = hit.collider.GetComponentInParent<Chest>();

                if (chest != null && !chest.isOpen)
                {
                    SetFocus(chest, hit);
                    return;
                }
            }
        }

        ClearFocus();
    }

    private void SetFocus(Chest chest, RaycastHit hit)
    {
        focusedChest = chest;

        if (interactPromptUI)
        {
            interactPromptUI.SetActive(true);

            int cost = chest.chestCost;
            int money = CurrencyManager.current.money;

            promptText.text = money >= cost
                ? promptText.text = $"Press '{interactKey}' to open chest (Cost: {cost})"
                : promptText.text = $"Not enough money to open chest (Cost: {cost})";
        }
    }
    
    private void ClearFocus()
    {
        if (focusedChest == null) return;
        focusedChest = null;

        if (interactPromptUI)
            interactPromptUI.SetActive(false);
    }
}
