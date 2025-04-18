using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CJMovementWithRB : MonoBehaviour
{
    public Rigidbody rb;
    public ParticleSystem movementDust;
    public ParticleSystem jumpLandDust;
    public ParticleSystem changeDust;

    public float playerSpeed = 3;
    int jumpForce;
    [SerializeField] float jumpHeight = 5;

    //These control how quickly the player reaches max speed and stops
    [SerializeField] float playerAcceleration = 5;
    [SerializeField] float playerDecceleration = 10;

    //This cant be greater than 1. This is used to control how quickly the player reaches max acceleration
    [SerializeField] float accelerationPower = 1;

    Vector3 moveInput;
    bool isJumping = false;
    bool wasGrounded;
    bool didMinJump = false;

    public bool swapCD = false;


    [SerializeField] float gravityScale;
    [SerializeField] float gravityScaleBase = 1;
    [SerializeField] float fallGravityScale = 3;


    float gravityValue;

    bool isDustPlaying;

    float jumpBuffer = 0.1f;
    float jumpBufferTimer = -0.1f;
    float coyoteTime = 0.1f;
    float lastGroundedTime = -0.1f;

    [SerializeField] private Vector3 groundCheckSize = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField] private Vector3 groundCheckOffset = new Vector3(0, -0.6f, 0);
    [SerializeField] private LayerMask groundLayer;
    public bool isGrounded;

    public GameObject MaxObject;
    public GameObject EvieObject;

    bool IsMax = true;

    //Sound Effects
    public AudioSource AS; //AS = Audio Source
    public AudioClip SlingSound, GrappleSound, JumpSound, CoinSound, DamageSound, CheckpointSound, BounceSound, EraseSound, StartSound, EnemyDeathSound;


    public bool facingRight;

    [SerializeField] CamFollowManager cammeraManager;

    //used to decide when to pan the camera downwards CJ
    public float fallSpeedThreshhold;

    public bool inLookDownZone;

    public static Vector3 Checkpoint;
    public bool hasWon = false;

    [SerializeField] AnimationHandler animationHandler;

   

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();

        /*PlaySound(StartSound);*/
        SoundHandler.instance.PlaySound(StartSound, transform, 0.3f);
        

        /*animator.SetBool("isGrounded", true);
        animator.SetBool("isJumping", false);
        animator.SetBool("isFalling", false);*/

        //fallSpeedThreshhold = VerticalCamManager.instance._fallSpeedYDampingChangeThreshold;


    }

    private void Awake()
    {
        isJumping = false;

        if (CheckpointStore.instance != null)
        {
            if (CheckpointStore.instance.GetActiveCheckpoint() != Vector3.zero)
            {
                transform.position = CheckpointStore.instance.GetActiveCheckpoint();
            }
            else
            {
                CheckpointStore.instance.AddCheckpoint(new Checkpoint(transform.position));
            }
        }
    }

    private void FixedUpdate()
    {
        if (!SpawnAnimation.isSpawning)
        {



            if (rb.velocity.y < 0)
            {
                gravityScale = fallGravityScale;
            }
            else
            {
                gravityScale = gravityScaleBase;
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


            if (!isJumping && rb.velocity.y > 0 && !isGrounded)
            {
                rb.AddForce(Vector2.down * 5);
            }

            //Last grounded time responable for coyote time CJ
            //jumpBufferTimer responsable for jump buffer. CJ
            if (lastGroundedTime >= 0f && jumpBufferTimer >= 0f && !isJumping)
            {
                JumpHandler();
            }

            //Temp Due to errors CD



            //If else statement is used to move the camera offset left and right based on what way the player is facing CJ
            if (!cammeraManager.inWinZone)
            {
                if (transform.rotation.y == 0)
                {
                    if (!facingRight)
                    {
                        cammeraManager.CallTurn();

                    }
                    facingRight = true;

                }
                else
                {
                    if (facingRight)
                    {
                        cammeraManager.CallTurn();

                    }
                    facingRight = false;

                }
            }

            //if else statement used to move the camera up and down depending on if the player is falling or not CJ
            if (rb.velocity.y < -10)
            {
                cammeraManager.currentYOffset = -4;
                cammeraManager.CallLookDown();

            }
            else if (!inLookDownZone)
            {
                cammeraManager.currentYOffset = 1.5f;
                cammeraManager.CallLookDown();
            }

        }
        


        
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("Player Speed: " + rb.velocity.magnitude);
        if (!SpawnAnimation.isSpawning)
        {
            /*Destroy(IntroPanels, IntroLife);*/

            //bool my beloved
            if (!hasWon)
            {
                isGrounded = CheckIfGrounded();
            }
            else
            {
                isGrounded = false;
            }
                moveInput = new Vector3(InputHandler.MovementDir.x, 0, 0);

            //Player Model Swap
            if (!swapCD)
            {
                

                if ((InputHandler.Ability1Pressed))
                {
                    if (IsMax)
                    {
                        MaxObject.SetActive(false);
                        EvieObject.SetActive(true);
                        IsMax = false;
                        changeDust.Play();
                        if (animationHandler != null)
                        {
                            animationHandler.SwapAnimatorTo(1);
                            animationHandler.ChangeAnimationTo("Attacking");
                            SoundHandler.instance.PlaySound(GrappleSound, transform, 0.5f);
                        }
                    }
                    else if (animationHandler != null)
                    {
                        animationHandler.SwapAnimatorTo(1);
                        animationHandler.ChangeAnimationTo("Attacking");
                        SoundHandler.instance.PlaySound(GrappleSound, transform, 0.5f);
                    }
                    StartCoroutine(SwapCooldown());
                }
                if ((InputHandler.Ability2Pressed))
                {
                    if (!IsMax)
                    {

                        MaxObject.SetActive(true);
                        EvieObject.SetActive(false);
                        IsMax = true;
                        changeDust.Play();
                        if (animationHandler != null)
                        {
                            animationHandler.SwapAnimatorTo(2);
                            animationHandler.ChangeAnimationTo("Attacking");
                            SoundHandler.instance.PlaySound(SlingSound, transform, 0.75f);
                        }
                    }
                    else if (animationHandler != null)
                    {
                        animationHandler.SwapAnimatorTo(2);
                        animationHandler.ChangeAnimationTo("Attacking");
                        SoundHandler.instance.PlaySound(SlingSound, transform, 0.75f);
                    }
                    StartCoroutine(SwapCooldown());
                }

                
            }

            /*if (moveInput.x < 0 && isGrounded || moveInput.x > 0 && isGrounded)
            {
                *//*animator.Play("Walk");*//*
                ChangeAnimationTo("Walk");
            }
            else if (moveInput.x == 0 || isGrounded)
            {
                *//*animator.SetBool("isMoving?", false);*//*
                ChangeAnimationTo("Idle 0");
            }*/

            //changed for skill swapping CD

            if (isGrounded)
            {
                if (!wasGrounded)
                {
                    CreateLandDust();

                }
                lastGroundedTime = coyoteTime;
            }
            else
            {
                lastGroundedTime -= Time.deltaTime;
            }
            if (InputHandler.JumpPressed)
            {
                jumpBufferTimer = jumpBuffer;
            }
            else
            {
                jumpBufferTimer -= Time.deltaTime;
            }

            if (!InputHandler.JumpHeld && rb.velocity.y > 0 && didMinJump && isJumping)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);
                isJumping = false;
            }

            /*if (InputHandler.JumpReleased)
            {
                isJumping = false;

            }*/






            //plays dust when player is running full speed
            if (Mathf.Abs(rb.velocity.x) >= playerSpeed - 4 && isGrounded)
            {

                CreateDust();
            }
            else
            {
                StopDust();
            }

            //rotates the character model to face the direction of movement CJ


            if (moveInput.x != 0)
            {
                transform.rotation = Quaternion.Euler(0, moveInput.x > 0 ? 0 : 180, 0);
            }

            wasGrounded = isGrounded;



            //if(rb.velocity.y <= fallSpeedThreshhold && !VerticalCamManager.instance.IsLerpingYDamping)
            //{
            //    VerticalCamManager.instance.LerpYDamping(true);
            //}

            //if(rb.velocity.y >= -1f && !VerticalCamManager.instance.IsLerpingYDamping && VerticalCamManager.instance.LerpedFromPlayerFalling)
            //{
            //    VerticalCamManager.instance.LerpedFromPlayerFalling = false;

            //    VerticalCamManager.instance.LerpYDamping(false);
            //}


        }
    }

    void JumpHandler()
    {



        /*animator.Play("Jumping");*/
        /*animator.SetBool("isGrounded", false);
        animator.SetBool("isJumping", true);
        animator.SetBool("isFalling", false);*/
        //animator.SetBool("isMoving?", false);

        /*PlaySound(JumpSound);*/
        SoundHandler.instance.PlaySound(JumpSound, transform, 0.75f);

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                float jumpForce = Mathf.Sqrt(jumpHeight * (Physics.gravity.y * -2));
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isJumping = true;
            jumpBufferTimer = 0f;
            StartCoroutine(JumpCooldown());
            StartCoroutine(MinJumpTime());





    }

    private bool CheckIfGrounded()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + groundCheckOffset, groundCheckSize / 2, Quaternion.identity, groundLayer);
        return colliders.Length > 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; 
        Gizmos.DrawWireCube(transform.position + groundCheckOffset, groundCheckSize);
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BouncePad"))
        {
            PlaySound(BounceSound);
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            PlaySound(WinSound);
        }

        if (collision.gameObject.CompareTag("HurtBox"))
        {
            PlaySound(DamageSound);
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            PlaySound(CoinSound);
        }
    }*/
    void CreateDust()
    {
        if (!isDustPlaying)
        {
            movementDust.Play();
        }
        isDustPlaying = true;
       

    }

    void StopDust()
    {
        movementDust.Stop(); 
        isDustPlaying = false;
    }

    void CreateLandDust()
    {
        Instantiate(jumpLandDust, new Vector3(transform.position.x, transform.position.y -0.5f, transform.position.z), Quaternion.identity);
    }


    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }

    private IEnumerator MinJumpTime()
    {
        didMinJump = false;
        yield return new WaitForSeconds(0.2f);
        didMinJump = true;
    }

    private IEnumerator SwapCooldown()
    {
        swapCD = true;
        Debug.LogWarning("Before Yippie");
        yield return new WaitForSeconds(0.5f);
        swapCD = false;
        Debug.LogWarning("After Yippie");
    }


}
