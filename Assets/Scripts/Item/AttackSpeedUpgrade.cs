using UnityEngine;

public class AttackSpeedUpgrade : GeneralItem
{
    [Header("references")]
    public GameObject primary;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        primary = GameObject.FindGameObjectWithTag("Primary");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void giveItem()
    {
        Rifle rifle = primary.GetComponentInChildren<Rifle>();
        rifle.timeBetweenShooting *= 0.5f;
    }
}
