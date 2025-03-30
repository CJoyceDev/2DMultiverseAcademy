using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CJMovingPlatform : MonoBehaviour
{
    
   [SerializeField] Vector3 PosLeft, PosRight;
    public float PlatformMoveSpeed;
    Rigidbody rb;
    public float CurrentMovement;
    //public GameObject platform;

    public bool MoveRight;
    private Vector3 lastPosition;
    public Vector3 velocity;
    public string PlatformName;

    

    public GameObject ghostPlatform;
    LineRenderer lineRenderer;


    [SerializeField] bool needsActivated;
    [HideInInspector] public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        lastPosition = transform.position;
        lineRenderer = GetComponent<LineRenderer>();
        PosLeft = new Vector3(PosLeft.x, transform.position.y, 0);
        PosRight = new Vector3(PosRight.x, transform.position.y, 0);

        //spawns the ghost platforms CJ
        //Instantiate(ghostPlatform, PosLeft, Quaternion.identity);
        //Instantiate(ghostPlatform, PosRight, Quaternion.identity);


        Vector3[] positions = new Vector3[]
              {
                PosLeft,
                PosRight
              };

        lineRenderer.SetPositions(positions);

    }

    // Fixed update required to stop frame rate affecting movement speed CJ
    // Velocity needed to add to character movement whilst they are on the platform CJ
    void FixedUpdate()
    {
        if (!needsActivated)
        {
            PlatformPatrol();
        }
        else if (isActive)
        {
            PlatformPatrol();
        }
        
        velocity = transform.position - lastPosition;
        lastPosition = transform.position;
    }


    void PlatformPatrol()
    {
        //returns a value betwwen 0 and 1 depending how close the platform is to the edges cj
        float t = Mathf.InverseLerp(PosLeft.x, PosRight.x, transform.position.x);

        //Sin graph = 1 at 90' and  0 at 0 or 180' cj
        float speedMultiplier = Mathf.Sin(t * Mathf.PI); 

    

        

        //Vector3 movement = new Vector3(adjustedSpeed, 0f, 0f);
        Vector3 curPosition = transform.position;
        Vector3 newPosition = curPosition;

        float distanceToRight = PosRight.x - curPosition.x;
        float distanceToLeft = curPosition.x - PosLeft.x;
        float adjustedSpeed;

        //Slows at edges to stop player being jolter off platform CJ
        if (distanceToRight <= 1 || distanceToLeft <=1)
        {
            adjustedSpeed = 1;
        }
        else
        {
            adjustedSpeed = PlatformMoveSpeed;
        }

        if (curPosition.x <= PosLeft.x)
        {
            MoveRight = true;
        }
        if (curPosition.x >= PosRight.x)
        {
            MoveRight = false;
        }

       
        if (MoveRight)
        {
            rb.MovePosition(rb.position + new Vector3(adjustedSpeed, 0f, 0f) * Time.fixedDeltaTime);
            //newPosition = curPosition + movement;
        }
        else
        {
            rb.MovePosition(rb.position - new Vector3(adjustedSpeed, 0f, 0f) * Time.fixedDeltaTime);
        }

       // rb.MovePosition(newPosition);
    }








}
