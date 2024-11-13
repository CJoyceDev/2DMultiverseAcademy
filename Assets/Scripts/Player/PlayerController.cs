using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;

public class PlayerController : MonoBehaviour
{

    //Testing Abilities for Evie //PD
    [SerializeField] bool glideB, slideB, pullBoxB, projectyleB, createObjectB;
    [SerializeField] Glide glideS;
    [SerializeField] Slide slideS;
    [SerializeField] PullBox pullBoxS;
    [SerializeField] Projectyle projectyleS;
    [SerializeField] CreateObject createObjectS;



    Animator animator;
    Collider coll;

    //Attack variables RS
    public GameObject AttackCollider;
    public GameObject AttackPoint;

   [SerializeField] float walkSpeed = 8f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float bouncePadBoost = 10f;
    [SerializeField] float EvieMass;
    [SerializeField] float MaxMass;

    bool IsMax = true, canSwap = true;

    public GameObject MaxObject;
    public GameObject EvieObject;

    bool isGrounded = true;
    bool isjumpQol = false;
    [SerializeField] LayerMask groundLayer;
   

    Rigidbody rb;

    bool pressedJump = false;

    private Vector3 SpawnPoint;
    public Vector3 curPosition;
    public Vector3 newPosition;
    public Vector3 platformVelocity;

    private bool onPlatform = false;

    public string CurrentPlatform;

    bool doubleJump;
    int jumpCharges;
    int jumpChargesMax = 2;

    private bool canJump = true;

    PlayerPauseUI ppUI; //most readable shortened word ever, you are a welcome //PD

    

    //Don't Touch, Needed For Inputs for the "new" system //PD
    public InputActions inputActions;
    private void Awake()
    {
        inputActions = new InputActions(); //!!!! //PD

        //Sets the vector for the spawn function to where ever Max was placed in the editor CJ
        SpawnPoint = transform.position;
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        animator = GetComponent<Animator>();
        ppUI = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerPauseUI>();
        
        //print(CurrentPlatform);
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
    //end of no touching //PD

   

    // Calls the control functions each Frame CJ
    //Control handlers use axis instead of getinput key to allow use of input methods other than keyboard CJ
    void Update()
    {
        glideS.abilityEnabled = glideB ? true : false;
        slideS.abilityEnabled = slideB ? true : false;
        pullBoxS.abilityEnabled = pullBoxB ? true : false;
        projectyleS.abilityEnabled = projectyleB ? true : false;
        createObjectS.abilityEnabled = createObjectB ? true : false;






        //Max Mechanic Code RS
        if (inputActions.Player.Ability.ReadValue<float>() > 0)
        {
            if (IsMax)
            {
                UnityEngine.Debug.Log("Attack");
                Instantiate(AttackCollider, AttackPoint.transform.position, Quaternion.identity);//Spawns AttackCollider
            }
            else
            {
                glideS.ActivateAbility();
                slideS.ActivateAbility();
                pullBoxS.ActivateAbility();
                projectyleS.ActivateAbility();
                createObjectS.ActivateAbility();
            }
        }
        
    }

    private void FixedUpdate()
    {
        WalkHandler();
        JumpHandler();

        CharacterSwapper();
        if (isGrounded && pressedJump == false )
        {
            doubleJump = false;
        }
       

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

        Vector3 newPosition = new Vector3();
        
        //this if statement is responsible for making the character move with the platform. CJ
        if (onPlatform == true)
        {
             newPosition = curPosition + movement + platformVelocity;
        }
        else
        {
             newPosition = curPosition + movement;
        }

         //newPosition = curPosition + movement;

        //moves the character model CJ
        rb.MovePosition(newPosition);
        
        //rotates the character model to face the direction of movement CJ
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }

   

       
    }

    //Character swapping, Checks for input and if the cooldown bool canSwap is true and does the magic //PD
    public void CharacterSwapper()
    {

        bool swapButton = inputActions.Player.Swap.ReadValue<float>() > 0;

        if (swapButton && canSwap)
        {
            
            if (IsMax)
            {
                EvieObject.SetActive(true);
                MaxObject.SetActive(false);
                IsMax = false;
                rb.mass = EvieMass;
            }
            else
            {
                EvieObject.SetActive(false);
                MaxObject.SetActive(true);
                IsMax = true;
                rb.mass = MaxMass;
            }
            canSwap = false;
            StartCoroutine(CoolDown());
        }
        
    }

    //responible for jump mechanics. animator.setbool is used to access the animation controller CJ
    void JumpHandler() 
    {
        float jAxis = inputActions.Player.Jump.ReadValue<float>();
        if (IsMax)
        {

            /*float jAxis = Input.GetAxis("Jump");*/
           

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
                if (!pressedJump && (isGrounded || isjumpQol))
                {

                    pressedJump = true;

                    Vector3 jumpVector = new Vector3(0f, jumpSpeed, 0f);


                    rb.velocity = jumpVector;

                    jumpCharges--;

                    animator.SetBool("isJumping", true);
                }
            }
            else
            {

                pressedJump = false;
            }

            if (rb.velocity.y < 0f)
            {

                animator.SetBool("isFalling", true);
            }

           
        }
        else
        {

            isGrounded = CheckGrounded();
            if (isGrounded)
            {
                animator.SetBool("isGrounded", true);
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", false);
            }

            if (jAxis > 0f )
            {
                //Used for evie double jump. CJ
                /*if ((isGrounded || isjumpQol) || doubleJump)
                {    

                        pressedJump = true;

                        Vector3 jumpVector = new Vector3(0f, jumpSpeed, 0f);
                        doubleJump = !doubleJump;
                        Vector3 jumpReset = new Vector3(1f, 0f, 1f);
                            
                        rb.velocity = new Vector3(rb.velocity.x, jumpVector.y, rb.velocity.z );
                        animator.SetBool("isJumping", true);
*//*                            canJump = false;*//*
                        StartCoroutine(JumpCoolDown()); 
                    
                }*/
                
                if (!pressedJump && (isGrounded || isjumpQol || jumpCharges > 0))
                {

                    pressedJump = true;

                    Vector3 jumpVector = new Vector3(0f, jumpSpeed, 0f);


                    rb.velocity = jumpVector;

                    jumpCharges--;

                    animator.SetBool("isJumping", true);
                }

            }
            else
            {
                pressedJump = false;
            }

            if (rb.velocity.y < 0f)
            {

                animator.SetBool("isFalling", true);
            }
        }
    }

    //if vertical movement = 0 character is grounded CJ

    //Now it checks for the ground with 3 rays or something, also a 0.1s time to jump after not being on a platfor cuz quality of life, does not like the interaction with RB gravity doe //PD
    bool CheckGrounded()
    {
        /*return GetComponent<Rigidbody>().velocity.y == 0f;*/
        /*Physics.Raycast(transform.position + new Vector3(i / 5, 0f, 0f), Vector3.down, out hit, 0.2f, groundLayer);*/

        Ray[] ray = new Ray[3];

        for (int i = -1; i < 2; i++)
        {


            ray[i+1] = new Ray(transform.position + new Vector3(i/1.6f, 0f, 0f), Vector3.down);
            if (Physics.Raycast(ray[i+1], 0.2f, groundLayer))
            {
                isjumpQol = true;
                jumpCharges = jumpChargesMax;
                return true;
            }

        }

        StartCoroutine(JumpQol());
        return false;

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


        if (other.CompareTag("MovingPlatform"))
        {
            
            onPlatform = true;
            CheckMovingPlatform();
            

        }

        if (other.CompareTag("BouncePad"))
        {
             
            jumpSpeed = bouncePadBoost;

        }
        
        
    }


    /// Causes unity to crash after hitting an objects trigger. check why CD

    // While standing on a movingplatform the platform's current velocity is set to a variable used by the movement function in order to keep the player on the platform CJ
    //Current platform is found with the CheckPlatform function CJ
    void OnTriggerStay(Collider other)
    {
        
      
          if (other.gameObject.layer == groundLayer)
            {
                print(CurrentPlatform);
            platformVelocity = GameObject.Find(CurrentPlatform).GetComponent<CJMovingPlatform>().velocity;



                /*if (GameObject.Find(CurrentPlatform))
                {
                    UnityEngine.Debug.Log("Found " + CurrentPlatform);
                } */

            }
        
       
    }


    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("MovingPlatform"))
        { 
            onPlatform = false;
           
        }

        if(other.CompareTag("BouncePad"))
        { 
            jumpSpeed = 5f;
        }

    }
    
    
    //Called whenever player touches a killbox CJ
    void Kill()
    {
        /*Spawn();*/
        print("kill");
        ppUI.DeathUI();

    }

    //Called on reaching end of level CJ
    void Win()
    {
        print("Win");
        /*Spawn();*/
        ppUI.WinUI();
    }

    //Can be used to set player back to spawn which is wherever Max is placed in the scene CJ

    //Sorry i broke it //PD
    public void Respawn()
    {

        transform.position = SpawnPoint;

    }

    //Checkpoint code
    public void OnCollisionEnter(Collision Checkpoint)
    {
        if (Checkpoint.gameObject.tag == "Checkpoint")
        {
            Checkpoint.gameObject.SetActive(false);
            //don't remove UnityEngine. it breaks the debug if it's not there
            UnityEngine.Debug.Log("Checkpoint Hit");
            SpawnPoint = Checkpoint.transform.position;
        }
    }

    //Character swapping too fast, made a cooldown //PD
    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(.5f);
        canSwap = true;
    }

    //This function uses info provided by the raycast in order to find out which moving platform the player is standing on CJ
    void CheckMovingPlatform()
    {
        RaycastHit hit;
        Vector3 TempPos = transform.position;

        TempPos.y = TempPos.y + 5f;
        
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 10f))
        {
            print(hit.collider.gameObject.name);
            CurrentPlatform = hit.collider.gameObject.name;
        }
        else
        {
            print("Null");
        }
    }

    //Stops player from doing both jumps with one button press CJ
    IEnumerator JumpCoolDown()
    {
        yield return new WaitForSeconds(.2f);
        canJump = true;
    }

    //quality of life for jumping close of ledges //PD
    IEnumerator JumpQol()
    {
        yield return new WaitForSeconds(.1f);
        isjumpQol = false;

    }
}



