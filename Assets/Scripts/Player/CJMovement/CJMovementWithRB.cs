using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CJMovementWithRB : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] int playerSpeed = 3;
    int jumpForce;
    [SerializeField] int jumpHeight = 5;

    //These control how quickly the player reaches max speed and stops
    [SerializeField] int playerAcceleration = 5;
    [SerializeField] int playerDecceleration = 10;

    //This cant be greater than 1. This is used to control how quickly the player reaches max acceleration
    [SerializeField] int accelerationPower = 1;

    Vector3 moveInput;
    bool isJumping;

    float gravityScale = 1;
    float gravityValue;

    float lastGroundedTime;
    float lastJumpedTime;
    float jumpPressedTime;
   float jumpPressedWindow = 0.1f;

    float jumpBuffer = 0.1f;
    float coyoteTime = 0.1f;

    [SerializeField] private Vector3 groundCheckSize = new Vector3(0.5f, 0.1f, 0.5f);
    [SerializeField] private Vector3 groundCheckOffset = new Vector3(0, -0.6f, 0);
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;

    //next step add ground detection and turn gravity off when grounded
    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        
        if (rb.velocity.y < 0)
        {
            gravityScale = 3;
        }
        else
        {
            gravityScale = 1;
        }

        float targetSpeed = moveInput.x * playerSpeed;
        float velocityDif = targetSpeed - rb.velocity.x;
        float accelerationRate;
        if (Mathf.Abs(targetSpeed) > 0.01f)
        {
            accelerationRate = playerAcceleration;
        }
        else
        {
            accelerationRate = playerDecceleration;
        }

        float playerMovement = Mathf.Pow(Mathf.Abs(velocityDif) * accelerationRate, accelerationPower) * Mathf.Sign(velocityDif);

        //simulates gravity on player CJ
        rb.AddForce(Vector3.up * Physics.gravity.y * gravityScale, ForceMode.Acceleration);
        rb.AddForce(new Vector3(playerMovement, 0, 0));

       
        
    }
    // Update is called once per frame
    void Update()
    {
        
        lastGroundedTime -= Time.deltaTime;
        lastJumpedTime -= Time.deltaTime;

        isGrounded = CheckIfGrounded();
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);


        if (isGrounded)
        {
            lastGroundedTime = coyoteTime;
            
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpHandler();
            jumpPressedTime = 0;

        }
        else if (Input.GetKeyUp(KeyCode.Space))
        { 
            isJumping = false;
        }

        if (!isJumping && rb.velocity.y > 0)
        {
            rb.AddForce(Vector2.down * 10);
        }
        
        //try increasing gravity as soon as the player lets go of space cj

 
       

    }

    void JumpHandler()
        {
            if (lastGroundedTime > 0 || lastJumpedTime > 0)
            {
                float jumpForce = Mathf.Sqrt(jumpHeight * (Physics.gravity.y * -2));
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isJumping = true;
                lastJumpedTime = jumpBuffer;
            }

        }


    private bool CheckIfGrounded()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + groundCheckOffset, groundCheckSize / 2, Quaternion.identity, groundLayer);
        return colliders.Length > 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Choose any color you like
        Gizmos.DrawWireCube(transform.position + groundCheckOffset, groundCheckSize);
    }
}
