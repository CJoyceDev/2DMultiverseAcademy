using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

//"Grappling Hook" (https://skfb.ly/6DvAF) by Leafdroid is licensed under Creative Commons Attribution (http://creativecommons.org/licenses/by/4.0/).
public class Hook : MonoBehaviour
{
    [SerializeField] float hookForce = 2f;

    Grappler grappler;
    Rigidbody rb;
    LineRenderer lineRenderer;
    Vector3 NewPos;
    public int resolution = 30;

    public int maxPoints = 2;
    private List<Vector3> ropePoints = new List<Vector3>();

    Transform lineStart;

    public void Initialize(Grappler grappler, Transform shootTransform, Transform lineStart)
    {
        transform.forward = shootTransform.forward;
        this.grappler = grappler;
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        rb.drag = 0.5f;
        rb.AddForce(transform.forward * hookForce, ForceMode.Impulse);
        this.lineStart = lineStart;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        NewPos = new Vector3(lineStart.transform.position.x, lineStart.transform.position.y, lineStart.transform.position.z);


        Vector3[] positions = new Vector3[]
              {
                  rb.transform.position,
                  NewPos
              };

        lineRenderer.SetPositions(positions);
       

        //UpdateRopeTrail();
    }

   

    void UpdateRopeTrail()
    {
       
        ropePoints.Add(transform.position);

        if (ropePoints.Count > maxPoints)
        {
            ropePoints.RemoveAt(0); 
        }

        lineRenderer.positionCount = ropePoints.Count + 1;
        lineRenderer.SetPosition(0, lineStart.position);

        for (int i = 0; i < ropePoints.Count; i++)
        {
            lineRenderer.SetPosition(i + 1, ropePoints[i]);
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
            grappler.ReturnHook = true;

        }

        if (other.CompareTag("KillBox"))
        {
            grappler.ReturnHook = true;

        }

    }

    
}
