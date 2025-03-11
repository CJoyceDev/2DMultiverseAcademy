using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovingPlatform : MonoBehaviour
{

    [SerializeField] Vector3 PosTop, PosBottom;
    public float PlatformMoveSpeed;
    Rigidbody rb;
    public float CurrentMovement;
    //public GameObject platform;

    public bool MoveUp;
    private Vector3 lastPosition;
    public Vector3 velocity;
    public string PlatformName;
    private bool stop;

    public GameObject ghostPlatform;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastPosition = transform.position;
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
    }


    void PlatformPatrol()
    {
        //gets the distance the model can move perframe CJ
        float distance = PlatformMoveSpeed * Time.deltaTime;
        /*float hAxis = Input.GetAxis("Horizontal");*/


        Vector3 movement = new Vector3(0f, distance, 0f);


        Vector3 curPosition = transform.position;
        //This newPosition line needed to stop asset from returning to origin on first itteration CJ
        Vector3 newPosition = transform.position;

        //Below ifs keeps asset between desired points CJ;
        if (curPosition.y <= PosBottom.y)
        {
            MoveUp = true;

        }
        if (curPosition.y >= PosTop.y)
        {
            MoveUp = false;
        }

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



}