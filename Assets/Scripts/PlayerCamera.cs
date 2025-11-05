using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] float sensX;
    [SerializeField] float sensY;

    [SerializeField] Transform orientation;

    float xRotation;
    float yRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;

        // Rotating the camera
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        // Rotating the player
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

    }
    
    // public void DoFov(float endValue)
    // {
    //     GetComponent<Camera>().DOFieldOfView(endValue, 0.25f);
    // }
}
