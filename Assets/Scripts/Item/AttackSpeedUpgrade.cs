using UnityEngine;

public class AttackSpeedUpgrade : GeneralItem
{
    [Header("references")]
    public GameObject primary;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    protected override void Start()
    {
        base.Start();
        primary = GameObject.FindGameObjectWithTag("Primary");
    }

    public override void giveItem()
    {
        Rifle rifle = primary.GetComponentInChildren<Rifle>();
        rifle.timeBetweenShooting *= 0.5f;
        Destroy(gameObject);
    }
}
