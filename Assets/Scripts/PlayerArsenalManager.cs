using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using System.Collections;

public class PlayerArsenalManager : MonoBehaviour
{
    [Header("References")]
    public Transform playerArsenal;
    public Animator handAnimator;
    public Transform handMesh;
    private List<GameObject> weapons;
    private int currentWeaponIndex;


    [Header("Swap setting")]
    public bool isSwapping = false;
    public float swapTime = 0.3f;
    public float rifleZAxis;
    public float revolverZAxis;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weapons = new List<GameObject>();


        foreach (Transform child in playerArsenal)
        {
            weapons.Add(child.gameObject);
        }

        // Primary weapon will be default to start
        currentWeaponIndex = 0;
        // disable all weapon except primary
        for (int i = 1; i < weapons.Count; i++)
        {
            weapons[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleWeaponSwapping();
    }

    private void HandleWeaponSwapping()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) {
                StopAllCoroutines();
                StartCoroutine(EquipWeapon(i));
            }
        }
    }

    private IEnumerator EquipWeapon(int index)
    {
        if (index == currentWeaponIndex)
            yield break;

        handAnimator.SetTrigger("Unequip");
        handAnimator.SetInteger("WeaponType", index);
        SetZAxis(index);

        isSwapping = true;

        // Turn off current weapon
        weapons[currentWeaponIndex].gameObject.SetActive(false);

        // Small delay for animation
        yield return new WaitForSeconds(swapTime);

        // Enable new weapon
        weapons[index].gameObject.SetActive(true);

        currentWeaponIndex = index;
        isSwapping = false; 
    }

    private void SetZAxis(int index)
    {
        if (index == 0)
        {
            handMesh.transform.localPosition = new Vector3(handMesh.transform.localPosition.x, handMesh.transform.localPosition.y, rifleZAxis);
        }
        else if (index == 1)
        {
            handMesh.transform.localPosition = new Vector3(handMesh.transform.localPosition.x, handMesh.transform.localPosition.y, revolverZAxis);
        }
    }
}
