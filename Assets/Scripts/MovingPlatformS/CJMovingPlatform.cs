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

    [SerializeField] bool needsActivated;
    [HideInInspector] public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        lastPosition = transform.position;

        PosLeft = new Vector3(PosLeft.x, transform.position.y, 0);
        PosRight = new Vector3(PosRight.x, transform.position.y, 0);

        //spawns the ghost platforms CJ
        Instantiate(ghostPlatform, PosLeft, Quaternion.identity);
        Instantiate(ghostPlatform, PosRight, Quaternion.identity);



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
        //returns a value betwwen 0 and 1 depending how close the platform is to the edges
        float t = Mathf.InverseLerp(PosLeft.x, PosRight.x, transform.position.x);

        //Sin graph = 1 at 90' and  0 at 0 or 180'
        float speedMultiplier = Mathf.Sin(t * Mathf.PI); 

       //0.5 and 1.5 are used to shift the sin graph so that speed never = 0
        float adjustedSpeed = PlatformMoveSpeed * Mathf.Lerp(0.5f, 1.5f, speedMultiplier) * Time.deltaTime;

        Vector3 movement = new Vector3(adjustedSpeed, 0f, 0f);
        Vector3 curPosition = transform.position;
        Vector3 newPosition = curPosition;

        
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
            newPosition = curPosition + movement;
        }
        else
        {
            newPosition = curPosition - movement;
        }

        rb.MovePosition(newPosition);
    }








}
