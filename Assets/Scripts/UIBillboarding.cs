using UnityEngine;

public class UIBillboarding : MonoBehaviour
{
    public Camera cam;
    public float sizeFactor = 0.05f; // tweak until it looks good
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = cam.transform.forward; 

        // Scale based on distance to camera
        float distance = Vector3.Distance(transform.position, cam.transform.position);
        transform.localScale = Vector3.one * distance * sizeFactor;
    }
}
