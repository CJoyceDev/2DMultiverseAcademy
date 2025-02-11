using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleRotate : MonoBehaviour
{
    // Start is called before the first frame update
    public string targetTag = "Box"; // Tag to look for
    public float radius = 5f; // Radius of the circle
    public int numberOfRays = 36; // Number of rays to cast
    float HitPoint;

    void Update()
    {
        CastRaysInCircle();
    }

    void CastRaysInCircle()
    {
        for (int i = 0; i < numberOfRays; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfRays;
            Vector3 direction = new Vector3(Mathf.Cos(angle) ,Mathf.Sin(angle), 0);
            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, radius))
            {
                if (hit.collider.CompareTag(targetTag))
                {
                    Debug.DrawRay(transform.position, direction * hit.distance, Color.red);
                    // Point towards the hit object
                    
                    transform.LookAt(hit.point);
                    break; // Stop after finding the first target
                }
                else
                {
                    Debug.DrawRay(transform.position, direction * hit.distance, Color.green);
                }
            }
            else
            {
                Debug.DrawRay(transform.position, direction * radius, Color.blue);
            }
        }
    }

}
