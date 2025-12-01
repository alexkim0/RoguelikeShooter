using UnityEngine;

public class UIBillboarding : MonoBehaviour
{
    public Camera cam;
    public float sizeFactor = 0.05f; // tweak until it looks good
    public float minSize = 0.5f;
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

        Vector3 resultScale = Vector3.one * distance * sizeFactor;
        transform.localScale = ClampVector(resultScale, Vector3.one * minSize, Vector3.one * 5f);

    }

    private Vector3 ClampVector(Vector3 v, Vector3 min, Vector3 max)
    {
        return new Vector3(
            Mathf.Clamp(v.x, min.x, max.x),
            Mathf.Clamp(v.y, min.y, max.y),
            Mathf.Clamp(v.z, min.z, max.z)
        );
    }
}
