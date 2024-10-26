using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{

    float walkSpeed = 1.0f;
    public GameObject FirstSpot;
    public GameObject SecondSpot;
    float walkingDirection = 1.0f;
    Vector3 walkAmount;
    Rigidbody rb;
    
    void Start()
    {
    rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        //Simple code for somthing moving back and forth, changing direction at 2 points CD
        walkAmount.x = walkingDirection * walkSpeed * Time.deltaTime;
        rb.velocity = rb.velocity + walkAmount;
        if (walkingDirection > 0.0f && transform.position.x >= SecondSpot.transform.position.x)
        {
           // rb.velocity = (0.0f, 0.0f, 0.0f);
            walkingDirection = -1.0f;

        }
        else if (walkingDirection < 0.0f && transform.position.x <= FirstSpot.transform.position.x)
        {
            //rb.velocity = (0.0f,0.0f,0.0f);
            walkingDirection = 1.0f;

        }

       // transform.Translate(walkAmount);

    }


}
