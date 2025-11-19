using TMPro;
using UnityEngine;

public class DamagePopUpGenerator : MonoBehaviour
{
    public static DamagePopUpGenerator currentGenerator;
    public GameObject popUpPrefab;

    void Awake()
    {
        currentGenerator = this;
    }

    void Update()
    {
        
    }

    public void CreateDamagePopUp(Vector3 position, string text)
    {
        GameObject popUp = Instantiate(popUpPrefab, position, Quaternion.identity);
        TextMeshProUGUI popUpText = popUp.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        popUpText.text = text;

        Destroy(popUp, 1f);
    }
}
