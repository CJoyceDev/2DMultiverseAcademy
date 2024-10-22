using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;

    public float walkSpeed = 8f;
    public float jumpSpeed = 7f;

    private bool isJumping;
    public bool isGrounded;

    Rigidbody rb;

    bool pressedJump = false;

    Collider coll;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        animator = GetComponent<Animator>();
    }

    // Calls the control functions each Frame CJ
    //Control handlers use axis instead of getinput key to allow use of input methods other than keyboard CJ
    void Update()
    {
        WalkHandler();
        JumpHandler();
    }

    
    void WalkHandler()
    {
        //gets the distance the model can move perframe CJ
        float distance = walkSpeed * Time.deltaTime;
        float hAxis = Input.GetAxis("Horizontal");

        print("Horizontal axis");
        print(hAxis);

        float vAxis = 0;

        //sets the movement vector equal to the distance multiplied by the input axis value. negative for moving left, 0 for standing still and positve for moving right CJ
        Vector3 movement = new Vector3(hAxis * distance, 0f, vAxis * distance);
       
       
        Vector3 curPosition = transform.position;

        Vector3 newPosition = curPosition + movement;

        //moves the character model CJ
        rb.MovePosition(newPosition);
        
        //rotates the character model to face the direction of movement CJ
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }

   

        print("Vertical axis");
        print(vAxis);
    }

    //responible for jump mechanics. animator.setbool is used to access the animation controller CJ
    void JumpHandler() 
    {
      
        float jAxis = Input.GetAxis("Jump");
        
        bool isGrounded = CheckGrounded();
        if (isGrounded)
        {
            animator.SetBool("isGrounded", true);
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
        
        if (jAxis > 0f)
        {
           //!pressed jump is needed to stop the player being able to fly by holding the button CJ
            if (!pressedJump && isGrounded)
            {
               
                pressedJump = true;
                
                Vector3 jumpVector = new Vector3(0f, jumpSpeed, 0f);
             
                rb.velocity = rb.velocity + jumpVector;
                animator.SetBool("isJumping", true);
            }
        }
        else
        {
            
            pressedJump = false;
        }

        if(rb.velocity.y < 0f)
        {
            isJumping = false;
            animator.SetBool("isFalling", true);
        }
    }
      
    //if vertical movement = 0 character is grounded CJ
    bool CheckGrounded()
    {
      
      return GetComponent<Rigidbody>().velocity.y == 0;
        
    }
}


