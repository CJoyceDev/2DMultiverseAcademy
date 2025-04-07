using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    public float shotForce;
    Rigidbody rb;


    public void Initialize(Transform shootTransform)
    {
        transform.forward = shootTransform.forward;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * shotForce, ForceMode.Impulse);
        Destroy(gameObject, 4f);
    }

}
