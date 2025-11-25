using UnityEngine;

public class LaserTrail : MonoBehaviour
{
    public float disappearTime = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, disappearTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
