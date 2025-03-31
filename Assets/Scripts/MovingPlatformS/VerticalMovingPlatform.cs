using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class VerticalMovingPlatform : MonoBehaviour
{

    [SerializeField] Vector3 PosTop, PosBottom;
    public float PlatformMoveSpeed;
    float startingSpeed;
    Rigidbody rb;
    public float CurrentMovement;
    //public GameObject platform;

    public bool MoveUp;
    private Vector3 lastPosition;
    public Vector3 velocity;
    public string PlatformName;
    private bool stop;

    public GameObject ghostPlatform;

    [SerializeField] private Vector3 groundCheckSize = new Vector3(0.1f, 2f, 0.1f);
    [SerializeField] private Vector3 groundCheckOffset = new Vector3(0, -0.6f, 0);
    [SerializeField] private LayerMask groundLayer;
    public bool isGrounded;

    BoxCollider crushBox;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastPosition = transform.position;
        startingSpeed = PlatformMoveSpeed;
        foreach (BoxCollider hitBox in GetComponentsInChildren<BoxCollider>())
        {
            if (hitBox.gameObject.name == "PlatformCrushBox")
            {
                crushBox = hitBox;

            }
        }
        /*Instantiate(ghostPlatform, PosTop, Quaternion.identity);
        Instantiate(ghostPlatform, PosBottom, Quaternion.identity);*/
    }

    // Fixed update required to stop frame rate affecting movement speed CJ
    // Velocity needed to add to character movement whilst they are on the platform CJ
    void FixedUpdate()
    {
        PlatformPatrol();
        velocity = transform.position - lastPosition;
        lastPosition = transform.position;
        if (crushBox != null)
        {
            if (CheckIfGrounded())
            {
                crushBox.enabled = true;
            }
            else
            {
                crushBox.enabled = false;
            }
        }
        
    }


    void PlatformPatrol()
    {
        //gets the distance the model can move perframe CJ
        //float distance = PlatformMoveSpeed * Time.deltaTime;
        /*float hAxis = Input.GetAxis("Horizontal");*/


       


        Vector3 curPosition = transform.position;
        //This newPosition line needed to stop asset from returning to origin on first itteration CJ
        Vector3 newPosition = transform.position;

        float distanceToTop = PosTop.y - curPosition.y;
        float distanceToBottom = curPosition.y - PosBottom.y;

        //Below ifs keeps asset between desired points CJ;
        if (curPosition.y <= PosBottom.y)
        {
            MoveUp = true;

        }
        if (curPosition.y >= PosTop.y)
        {
            MoveUp = false;
        }

        if (distanceToTop <= 2)
        {
            PlatformMoveSpeed = distanceToTop;
            if(distanceToTop <= 0.4f)
            {
                PlatformMoveSpeed = 0.4f;
            }
        }
        else if (distanceToBottom <= 2)
        {
            PlatformMoveSpeed = distanceToTop;
            if (distanceToBottom <= 0.4f)
            {
                PlatformMoveSpeed = 0.4f;
            }
        }
        else
        {
            PlatformMoveSpeed = startingSpeed;
        }


        float distance = PlatformMoveSpeed * Time.deltaTime;

        Vector3 movement = new Vector3(0f, distance, 0f);

        if (MoveUp)
        {
            newPosition = curPosition + movement;
            stop = true;
            StartCoroutine(StopMovement());
        }
        if (!MoveUp && !stop)
        {
            newPosition = curPosition - movement;
        }


        rb.MovePosition(newPosition);


    }

    IEnumerator StopMovement()
    {
        yield return new WaitForSeconds(2f);
        stop = false;
    }

    private bool CheckIfGrounded()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + groundCheckOffset, groundCheckSize / 2, Quaternion.identity, groundLayer);
        return colliders.Length > 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + groundCheckOffset, groundCheckSize / 2);
    }



}