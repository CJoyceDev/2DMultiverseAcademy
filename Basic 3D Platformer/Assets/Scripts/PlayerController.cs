using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;

    //Made Serialized CD
    [SerializeField] public float walkSpeed;
    [SerializeField] public float jumpSpeed;
    [SerializeField] public float maxspeed;
    [SerializeField] public float slowingFactor;

    private bool isJumping;
    public bool isGrounded;

    Rigidbody rb;

    bool pressedJump = false;

    Collider coll;

    private bool onMovingObject;
    private Rigidbody platformRB;

    // Start is called before the first frame update
    void Start()
    {

       walkSpeed = 8f;
       jumpSpeed = 7f;
        maxspeed = 5f;
        slowingFactor = 5f;
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        animator = GetComponent<Animator>();

        onMovingObject = false;

    }

// Calls the control functions each Frame CJ
//Control handlers use axis instead of getinput key to allow use of input methods other than keyboard CJ
void FixedUpdate()
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

        //rb-velocity does this CD
        //Vector3 curPosition = transform.position;
        //Vector3 newPosition = curPosition + movement;

        //moves the character model CJ
        //rb.MovePosition(newPosition);

        
        //changed movement CD
        rb.velocity = rb.velocity + movement;

        if (onMovingObject && platformRB != null)
        {
  
           rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, platformRB.velocity.x - maxspeed, platformRB.velocity.x + maxspeed), rb.velocity.y, Mathf.Clamp(rb.velocity.z, platformRB.velocity.z - maxspeed, platformRB.velocity.z + maxspeed));
        
        }
        else
        {
            rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxspeed, maxspeed), rb.velocity.y, Mathf.Clamp(rb.velocity.z, -maxspeed, maxspeed));
        }

        //make the Deceleration of the character faster and smother CD
        //If no input horizontally
        if (hAxis == 0)
        {
            //Slow faster
            if (rb.velocity.x != 0)
            {
                //If positive x velocity
                if (Mathf.Sign(rb.velocity.x) == 1)
                {
                    if (0 > rb.velocity.x)
                    {
                        rb.velocity = rb.velocity - new Vector3(slowingFactor, 0f, 0f);
                    }
                }
                else
                {
                    if (0 < rb.velocity.x)
                    {
                        rb.velocity = rb.velocity + new Vector3(slowingFactor, 0f, 0f);
                    }
                }
            }

            if (rb.velocity.z != 0)
            {
                //If positive z velocity
                if (Mathf.Sign(rb.velocity.z) == 1)
                {
                    if (0 > rb.velocity.z)
                    {
                        rb.velocity = rb.velocity - new Vector3(0f, 0f, slowingFactor);
                    }
                }
                else
                {
                    if (0 < rb.velocity.z)
                    {
                        rb.velocity = rb.velocity + new Vector3(0f, 0f, slowingFactor);
                    }
                }
            }
        }

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
                isGrounded = false;

                Vector3 jumpVector = new Vector3(0f, jumpSpeed, 0f);
             
                rb.velocity = rb.velocity + jumpVector;
                animator.SetBool("isJumping", true);
            }
        }
        else
        {
            
            pressedJump = false;
        }

        if(rb.velocity.y < 0f && !isGrounded)
        {
            isJumping = false;
            animator.SetBool("isFalling", true);
        }
    }

    //On initial collision
    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {

            //Debug.DrawRay(contact.point, contact.normal, Color.white);

            //If the normal of the collision is upwards CD
            //IE if your colliding from above
            //Note:Currently only works if the platform is perfetly horizontal, change this to be a range
            if (contact.normal == Vector3.up)
            {

                //Set player as grounded
                Debug.Log("Grounded"); ;
                isGrounded = true;

                //Check if object collided with has a rigidbody
                platformRB = collision.gameObject.GetComponent<Rigidbody>();

                //If it does
                if (platformRB != null)
                {
                    //For movement on a moving object CD
                    //Debug.Log(platformRB.velocity);
                    rb.velocity = platformRB.velocity;
                    onMovingObject = true;
                }

            }
        }

    }

    //On leaving collision
    void OnCollisionExit(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {

            //Debug.DrawRay(contact.point, contact.normal, Color.white);

            //If the normal of the collision is upwards 
            //IE if your colliding from above
            //Note:Currently only works if the platform is perfetly horizontal, change this to be a range
            if (contact.normal == Vector3.up)
            {

                //Set player as grounded
                Debug.Log("Not Grounded"); ;
                isGrounded = false;

                //Check if object collided with has a rigidbody
                platformRB = collision.gameObject.GetComponent<Rigidbody>();

                //If it does
                if (platformRB != null)
                {

                    onMovingObject = false;
                }

            }
        }

    }

}