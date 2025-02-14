using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementPD : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Rigidbody rb;
    [SerializeField] float Speed, jumpForce, maxSpeed;
    float playerInput;

    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;


    public bool wallSlideR = false, wallSlideL = false;


    // Update is called once per frame
    void Update()
    {

        playerInput = Input.GetAxisRaw("Horizontal");


    }

    private void FixedUpdate()
    {
        if (isGrounded())
        {
            rb.velocity += new Vector3(playerInput * Speed, 0f, 0f);
        }


        if (wallSlideL)
        {
            rb.velocity = new Vector3(0f,-3.5f,0f);
            if (Input.GetKey(KeyCode.Space))
            {
                rb.velocity = new Vector3(jumpForce, jumpForce, 0f);
            }
            if (Input.GetKey(KeyCode.D))
            {
                wallSlideL = false;
                rb.velocity = new Vector3(2f, -5f, 0f);
            }
        }
        if (wallSlideR)
        {
            rb.velocity = new Vector3(0f, -3.5f, 0f);
            if (Input.GetKey(KeyCode.Space))
            {
                rb.velocity = new Vector3(-jumpForce, jumpForce, 0f);
            }
            if (Input.GetKey(KeyCode.A))
            {
                wallSlideR = false;
                rb.velocity = new Vector3(-2f, -5f, 0f);
            }
        }


        if (rb.velocity.x > maxSpeed || rb.velocity.x < -maxSpeed)
        {
            rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y, 0f);

        }

        if (Input.GetKey(KeyCode.Space) && isGrounded() && (!wallSlideR || !wallSlideL))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0f);

        }

        FlipPlayer();

    }

    public bool isGrounded()
    {
        if (Physics.BoxCast(transform.position, boxSize / 2, -transform.up, transform.rotation, castDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

    private void FlipPlayer()
    {
        if (rb.velocity.x > 0.2)
        {
            player.transform.rotation = new Quaternion(0,0,0,0);
        }
        if (rb.velocity.x < -0.2)
        {
            player.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }
}
