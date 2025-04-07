using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    public float shotForce;
    Rigidbody rb;
    Rigidbody playerRB;
    Vector3 playerVelocity;

    public void Initialize(Transform shootTransform, Rigidbody playerRB)
    {
        playerVelocity = playerRB.velocity;
        transform.forward = shootTransform.forward;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * shotForce + new Vector3(playerVelocity.x, 0f, 0f), ForceMode.Impulse); // Player velocity stops the issues of the shot just falling out. CJ
        Destroy(gameObject, 4f);
    }

}
