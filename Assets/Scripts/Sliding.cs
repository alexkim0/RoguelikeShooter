using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Sliding : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerObject;
    private Rigidbody rb;
    private PlayerMovement playerMovement;

    [Header("Sliding")]
    // maximum time you are allowed to slide
    public float maxSlideTime;
    public float slideForce;
    // Timer to check how long player been sliding
    private float slideTimer;

    // will be half of startYScale
    public float slideYScale;
    private float startYScale;

    [Header("Input")]
    public KeyCode slideKey = KeyCode.LeftControl;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 inputDirection;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();

        startYScale = playerObject.localScale.y;
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // slide when control is clicked and either wasd is clicked
        // TODO: modifying later
        if (Input.GetKeyDown(slideKey) && (horizontalInput != 0 || verticalInput != 0) && playerMovement.grounded)
            StartSlide();

        if (Input.GetKeyUp(slideKey) && playerMovement.sliding)
            StopSlide();
    }

    void FixedUpdate()
    {
        if (playerMovement.sliding)
            SlidingMovement();
    }

    private void StartSlide()
    {
        playerMovement.sliding = true;

        // Shrink the player
        playerObject.localScale = new Vector3(playerObject.localScale.x, slideYScale, playerObject.localScale.z);
        // Apply downward force to avoid player floating when shrinking
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        // slideTimer = maxSlideTime;


    }

    private void SlidingMovement()
    {
        if (!playerMovement.grounded) return;
        
        // Gets slide direction depending on what input you press(wasd)
        

        // sliding on flat surface
        if (!playerMovement.OnSlope() || rb.linearVelocity.y > -0.1f)
        {
            rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);

            // slideTimer -= Time.deltaTime;
        }
        // sliding down a slope
        else
        {
            rb.AddForce(playerMovement.GetSlopeMoveDirection(inputDirection) * slideForce, ForceMode.Force);
        }



        // if (slideTimer <= 0)
        //     StopSlide();
    }
    
    private void StopSlide()
    {
        playerMovement.sliding = false;
        // Scale up the player back to normal
        playerObject.localScale = new Vector3(playerObject.localScale.x, startYScale, playerObject.localScale.z);
    }   
}
