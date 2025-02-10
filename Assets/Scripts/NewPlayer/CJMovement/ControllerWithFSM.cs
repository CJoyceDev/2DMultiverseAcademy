using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void Enter();
    public void Execute();
    public void Exit();
}

public class StateMachine
{
   public IState currentState;


    public void ChangeState(IState newState)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = newState;
        currentState.Enter();
    }

    public void Update()
    {
        if (currentState != null) currentState.Execute();
    }
}

public class TestState : IState
{
    ControllerWithFSM owner;

    public TestState(ControllerWithFSM owner) { this.owner = owner; }

    void IState.Enter()
    {
        Debug.Log("entering test state");
    }

    void IState.Execute()
    {
        Debug.Log("updating test state");
    }

    void IState.Exit()
    {
        Debug.Log("exiting test state");
    }
}



public class ControllerWithFSM : MonoBehaviour
{


    public Rigidbody rb;
    public ParticleSystem movementDust;

    public int playerSpeed = 3;
    int jumpForce;
    public int jumpHeight = 5;

    //These control how quickly the player reaches max speed and stops
    [SerializeField] int playerAcceleration = 5;
    [SerializeField] int playerDecceleration = 10;

    //This cant be greater than 1. This is used to control how quickly the player reaches max acceleration
    [SerializeField] int accelerationPower = 1;

    Vector3 moveInput;
    public bool isJumping;

    float gravityScale = 1;
    float gravityValue;

    float lastGroundedTime;
    public float lastJumpedTime;
    public float jumpPressedTime;
    public float jumpPressedWindow = 0.1f;
    bool isDustPlaying;

    public float jumpBuffer = 0.1f;
    float coyoteTime = 0.1f;

    [SerializeField] private Vector3 groundCheckSize = new Vector3(0.5f, 0.1f, 0.5f);
    [SerializeField] private Vector3 groundCheckOffset = new Vector3(0, -0.6f, 0);
    [SerializeField] private LayerMask groundLayer;
    public bool isGrounded;

    public GameObject MaxObject;

    Animator animator;

    StateMachine stateMachine = new StateMachine();

    void Start()
    {
        stateMachine.ChangeState(new TestState(this));
        rb = GetComponent<Rigidbody>();

        animator = MaxObject.GetComponent<Animator>();

        animator.SetBool("isGrounded", true);
        animator.SetBool("isJumping", false);
        animator.SetBool("isFalling", false);
    }

    void Update()
    {
        stateMachine.Update();
        if (isGrounded && !(stateMachine.currentState is Grounded))
        {
            stateMachine.ChangeState(new Grounded(this));

            animator.SetBool("isGrounded", true);
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (stateMachine.currentState is Grounded)
            {
                stateMachine.ChangeState(new Jumping(this));

                animator.SetBool("isGrounded", false);
                animator.SetBool("isJumping", true);
                animator.SetBool("isFalling", false);
            }


        }

      

        if (CheckIfGrounded()) 
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }


    private void FixedUpdate()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
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
        //Velocity differnce is the speed required to get to max speed or 0 based on the player input.CJ
        //Please see this great tutorial for where I got the formula https://www.youtube.com/watch?v=KbtcEVCM7bw&t=164s CJ
        //In brief Mathf.pow is required for non linear acceleration meaning more responsive controls and sign is required for positive and negative values to be calculated
        //not just possitive CJ
        float playerMovement = Mathf.Pow(Mathf.Abs(velocityDif) * accelerationRate, accelerationPower) * Mathf.Sign(velocityDif);

        //simulates gravity on player CJ
        rb.AddForce(Vector3.up * Physics.gravity.y * gravityScale, ForceMode.Acceleration);
        rb.AddForce(new Vector3(playerMovement, 0, 0));


    }


       public void CreateDust()
        {
            if (!isDustPlaying)
            {
                movementDust.Play();
            }
            isDustPlaying = true;


        }

        public void StopDust()
        {
            movementDust.Stop();
            isDustPlaying = false;
        }

        private bool CheckIfGrounded()
        {
        Collider[] colliders = Physics.OverlapBox(transform.position + groundCheckOffset, groundCheckSize / 2, Quaternion.identity, groundLayer);
        return colliders.Length > 0;
        }
}

    public class Grounded : IState
    {
        ControllerWithFSM owner;

        public Grounded(ControllerWithFSM owner) { this.owner = owner; }

        void IState.Enter()
        {
            Debug.Log("entering grounded state");
        }

        void IState.Execute()
        {
            if (Mathf.Abs(owner.rb.velocity.x) >= owner.playerSpeed - 4 && owner.isGrounded)
            {

                owner.CreateDust();
            }
            else
            {
                owner.StopDust();
            }


        }

        void IState.Exit()
        {
            Debug.Log("exiting test state");
        }
    }


public class Jumping : IState
{
    ControllerWithFSM owner;

    public Jumping(ControllerWithFSM owner) { this.owner = owner; }

    void IState.Enter()
    {
        owner.rb.velocity = new Vector3(owner.rb.velocity.x, 0f, owner.rb.velocity.z);
        float jumpForce = Mathf.Sqrt(owner.jumpHeight * (Physics.gravity.y * -2));
        owner.rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        owner.isJumping = true;
        owner.lastJumpedTime = owner.jumpBuffer;
    }

    void IState.Execute()
    {


    }

    void IState.Exit()
    {
        Debug.Log("exiting test state");
    }
}






