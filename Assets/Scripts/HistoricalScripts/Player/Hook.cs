using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//"Grappling Hook" (https://skfb.ly/6DvAF) by Leafdroid is licensed under Creative Commons Attribution (http://creativecommons.org/licenses/by/4.0/).
public class Hook : MonoBehaviour
{
    [SerializeField] float hookForce = 2f;

    Grappler grappler;
    Rigidbody rb;
    LineRenderer lineRenderer;
    Vector3 NewPos;

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

        NewPos = new Vector3(grappler.transform.position.x, grappler.transform.position.y + 0.5f, grappler.transform.position.z);
        

      Vector3[] positions = new Vector3[]
            {
                rb.transform.position,
                NewPos
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

        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);

        }
           

        }

    
}
