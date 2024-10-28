using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    Collider coll;
    

    public float walkSpeed = 8f;
    public float jumpSpeed = 7f;

   
    public bool isGrounded;

    Rigidbody rb;

    bool pressedJump = false;

    private Vector3 SpawnPoint;


    //Don't Touch, Needed For Inputs for the "new" system
    public InputActions inputActions;
    private void Awake()
    {
        inputActions = new InputActions();
        //Sets the vector for the spawn function to where ever Max was placed in the editor CJ
        SpawnPoint = transform.position;
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
    //end of no touching

   

    // Calls the control functions each Frame CJ
    //Control handlers use axis instead of getinput key to allow use of input methods other than keyboard CJ
    void Update()
    {
        
        
    }

    private void FixedUpdate()
    {
        WalkHandler();
        JumpHandler();
    }



    void WalkHandler()
    {
        //gets the distance the model can move perframe CJ
        float distance = walkSpeed * Time.deltaTime;
        /*float hAxis = Input.GetAxis("Horizontal");*/
        float hAxis = inputActions.Player.Move.ReadValue<Vector2>().x;



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

   

       
    }

    //responible for jump mechanics. animator.setbool is used to access the animation controller CJ
    void JumpHandler() 
    {

        /*float jAxis = Input.GetAxis("Jump");*/
        float jAxis = inputActions.Player.Jump.ReadValue<float>();

        isGrounded = CheckGrounded();
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
           
            animator.SetBool("isFalling", true);
        }
    }
      
    //if vertical movement = 0 character is grounded CJ
    bool CheckGrounded()
    {
      
      return GetComponent<Rigidbody>().velocity.y == 0f;
        
    }

    //if character hits collision hidden under level will set the character back to spawnCJ
    //using transform.position wil work for prototyping however start point will be different for each evel so will neeed changed. (Issue now fixed with spawn function) CJ
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillBox"))
        {
            Kill();
        }

        if (other.CompareTag("Finish"))
        {
            Win();
        }

    }
    
    
    //Called whenever player touches a killbox CJ
    void Kill()
    {
        Spawn();
        print("kill");
    }

    //Called on reaching end of level CJ
    void Win()
    {
        print("Win");
        Spawn();
    }

    //Can be used to set player back to spawn which is wherever Max is placed in the scene
    void Spawn()
    {

        transform.position = SpawnPoint;

        
    }
}



