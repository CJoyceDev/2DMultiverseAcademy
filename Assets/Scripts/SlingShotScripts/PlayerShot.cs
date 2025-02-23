using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    public float shotForce = 10000f;
    Rigidbody rb;
    Slingshot slingshot;

    public void Initialize(Slingshot slingshot, Transform shootTransform)
    {
        transform.forward = shootTransform.forward;
        this.slingshot = slingshot;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * shotForce, ForceMode.Impulse);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
