using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float LifeTime = 2;

    public static float ForceX = 15f;
    public static float ForceY = 3f;

    Vector3 PushForce = new Vector3(ForceX, ForceY, 0);
    Rigidbody rb;

    private void OnCollisionEnter(Collision other)
    {
        //Detects Enemy RS
        if (other.gameObject.tag == "KillBox")
        {
            //uncomment this to test pushing enemies away instead of killing them
            //rb = other.gameObject.GetComponent<Rigidbody>();
            //rb.AddForce(PushForce, ForceMode.Impulse);

            Debug.Log("Enemy Hit");

            //comment this out when testing pushing enemies away
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

        //Detects Player so that it doesn't block them after it appears RS
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            Debug.Log("Player Hit");
        }

        if (other.gameObject.tag == "Box")
        {
            rb = other.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(PushForce, ForceMode.Impulse);

            Destroy(this);
        }
    }

    private void Update()
    {
        Destroy(this.gameObject, LifeTime);//Removes Attack Collider if it hasn't collided with anything RS
    }
}
