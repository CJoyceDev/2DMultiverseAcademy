using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewHook : MonoBehaviour
{
    [SerializeField] float hookForce = 2f;

    NewGrappler grappler;
    Rigidbody rb;
    LineRenderer lineRenderer;
    Vector3 NewPos;

    public void Initialize(NewGrappler grappler, Transform shootTransform)
    {
        transform.forward = shootTransform.forward;
        this.grappler = grappler;
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        rb.drag = 0.5f;
        rb.AddForce(transform.forward * hookForce, ForceMode.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(grappler != null) { 
        NewPos = new Vector3(grappler.transform.position.x, grappler.transform.position.y, grappler.transform.position.z);


        Vector3[] positions = new Vector3[]
              {
                rb.transform.position,
                NewPos
              };

        lineRenderer.SetPositions(positions);

       }

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

        if (other.CompareTag("KillBox"))
        {
            Destroy(gameObject);

        }

    }

}
