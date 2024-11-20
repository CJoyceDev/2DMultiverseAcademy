using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField] float hookForce = 2f;

    Grappler grappler;
    Rigidbody rb;
    LineRenderer lineRenderer;

    public void Initialize(Grappler grappler, Transform shootTransform)
    {
        transform.forward = shootTransform.forward;
        this.grappler = grappler;
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        rb.AddForce(transform.forward * hookForce, ForceMode.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3[] positions = new Vector3[]
            {
                rb.transform.position,
                grappler.transform.position
            };

        lineRenderer.SetPositions(positions);
    }

    private void OnTriggerEnter(Collider other)
    {
      
        if (other.CompareTag("Box"))
        {

                  rb.useGravity = false;
                  rb.isKinematic = true;
                 grappler.StartPull(other);

         }
           

        }

    
}
