using UnityEngine;
using UnityEngine.Rendering;

public class Dashing : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerCam;
    private Rigidbody rb;
    private PlayerMovement playerMovement;

    [Header("Dashing")]
    public float dashForce;
    public float dashUpwardForce;
    public float maxDashYSpeed;
    public float dashDuration;

    // Multiple different settings for dashing to get different behavior    
    [Header("Settings")]
    public bool useCameraForward = true;
    public bool allowAllDirections = true;
    public bool disableGravity = false;
    public bool resetVelocity = true;

    [Header("Cooldown")]
    public float dashCooldown;
    public float dashCooldownTimer;

    [Header("CameraEffects")]
    public PlayerCamera playerCamera;
    public float dashFov;

    [Header("Input")]
    public KeyCode dashKey = KeyCode.LeftShift;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(dashKey))
        {
            Dash();
        }

        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    private void Dash()
    {
        if (dashCooldownTimer > 0) return;
        else dashCooldownTimer = dashCooldown;

        playerMovement.dashing = true;
        playerMovement.maxYSpeed = maxDashYSpeed;

        // playerCamera.DoFov(dashFov);

        Transform forwardTransform;

        if (useCameraForward)
            forwardTransform = playerCam;
        else
            forwardTransform = orientation;

        Vector3 direction = GetDirection(forwardTransform);

        Vector3 forceToApply = direction * dashForce + orientation.up * dashUpwardForce;

        if (disableGravity)
            rb.useGravity = false;

        delayedForceToApply = forceToApply;
        Invoke(nameof(DelayedDashForce), 0.025f);

        Invoke(nameof(ResetDash), dashDuration);
    }

    private Vector3 delayedForceToApply;
    private void DelayedDashForce()
    {
        // make velocity to 0 before dashing
        if (resetVelocity)
            rb.linearVelocity = Vector3.zero;
        
        rb.AddForce(delayedForceToApply, ForceMode.Impulse);

    }

    private void ResetDash()
    {
        playerMovement.dashing = false;
        playerMovement.maxYSpeed = 0;

        // playerCamera.DoFov(90f);

        if (disableGravity)
            rb.useGravity = true;
    }

    private Vector3 GetDirection(Transform forwardTransform)
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3();

        if (allowAllDirections)
        {
            direction = forwardTransform.forward * verticalInput + forwardTransform.right * horizontalInput;
        }
        else
        {
            direction = forwardTransform.forward;
        }

        if (verticalInput == 0 && horizontalInput == 0)
        {
            direction = forwardTransform.forward;
        }

        return direction.normalized;
    }

}
