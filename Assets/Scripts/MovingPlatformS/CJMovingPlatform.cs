using System.Collections;
using System.Collections.Generic;
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

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastPosition = transform.position;
    }

    // Fixed update required to stop frame rate affecting movement speed CJ
    // Velocity needed to add to character movement whilst they are on the platform CJ
    void FixedUpdate()
    {
        PlatformPatrol();
        velocity = transform.position - lastPosition;
        lastPosition = transform.position;
    }


    void PlatformPatrol()
    {
        //gets the distance the model can move perframe CJ
        float distance = PlatformMoveSpeed * Time.deltaTime;
        /*float hAxis = Input.GetAxis("Horizontal");*/


        Vector3 movement = new Vector3(distance, 0f, 0f);


        Vector3 curPosition = transform.position;
        //This newPosition line needed to stop asset from returning to origin on first itteration CJ
        Vector3 newPosition = transform.position;

        //Below ifs keeps asset between desired points CJ;
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
        if (!MoveRight)
        {
            newPosition = curPosition - movement;
        }


        rb.MovePosition(newPosition);


    }





}
