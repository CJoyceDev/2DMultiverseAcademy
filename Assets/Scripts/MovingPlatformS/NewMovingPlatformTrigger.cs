using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovingPlatformTrigger : MonoBehaviour
{

    public CJMovementWithRB cJMovementWithRB;
    Vector3 lastPosition;

    void Start()
    {
        cJMovementWithRB = FindObjectOfType<CJMovementWithRB>();
    }

     void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
            cJMovementWithRB.playerSpeed += 2; 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
            cJMovementWithRB.playerSpeed -= 2;
        }
    }

    //private void FixedUpdate()
    //{
    //    Vector3 platformVelocity = (transform.position - lastPosition) / Time.fixedDeltaTime;
    //    lastPosition = transform.position;


    //    foreach (Transform child in transform)
    //    {
    //        Rigidbody rb = child.GetComponent<Rigidbody>();
    //        if (rb != null)
    //        {
    //            rb.velocity = new Vector3(platformVelocity.x, rb.velocity.y, platformVelocity.z);
    //        }
    //    }
    //}
}

