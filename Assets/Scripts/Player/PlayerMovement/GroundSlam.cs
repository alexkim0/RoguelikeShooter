using UnityEngine;

public class GroundSlam : MonoBehaviour
{
    [Header("References")]
    private Rigidbody rb;
    private PlayerMovement playerMovement;

    [Header("Ground Slam")]
    public float slamForce;
    public float initialForce;
    public float superJumpTimeFrame = 0.1f;
    public float superJumpTimer;

    [Header("Super Jump")]
    public float superJumpMultiplier;
    //IR == increase rate
    public float superJumpMultiplierIR;
    public float maxSuperJumpMultiplier = 2f;

    [Header("Input")]
    public KeyCode slamKey = KeyCode.LeftControl;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerMovement.slamming && !playerMovement.grounded)
        {
            Slam();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(slamKey) && !playerMovement.slamming && !playerMovement.grounded)
        {
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(Vector3.down * initialForce, ForceMode.Impulse);
            playerMovement.slamming = true;
            superJumpTimer = superJumpTimeFrame;
            superJumpMultiplier = 0;
        }

        if (playerMovement.grounded)
        {
            playerMovement.slamming = false;
            // if player jumps in between the superJumpTimeFrame, it will do super jump in PlayerMovement.cs
            if (superJumpTimer > 0)
            {
                superJumpTimer -= Time.deltaTime;
            }
        }


    }

    private void Slam()
    {
        rb.AddForce(Vector3.down * slamForce, ForceMode.Force);
        superJumpMultiplier += Time.deltaTime * superJumpMultiplierIR;
        superJumpMultiplier = Mathf.Clamp(superJumpMultiplier, 0f, maxSuperJumpMultiplier);
    }
}

