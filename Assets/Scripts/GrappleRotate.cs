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
   public List<Vector3> positionsList = new List<Vector3>();
   public Vector3[] positions;
    Vector3 TargetSpot;
    // Quaternion target = Quaternion.Euler(-10, 0, 0);

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
            RaycastHit[] hits = Physics.RaycastAll(ray, radius);

            foreach (RaycastHit hit in hits)
               {
                if (hit.collider.CompareTag(targetTag))
                {
                    Debug.DrawRay(transform.position, direction * hit.distance, Color.red);
                    // Point towards the hit object
                    positionsList.Add(hit.point);
                     positions = positionsList.ToArray();
                      TargetSpot = GetMeanVector(positions);
                      transform.LookAt(TargetSpot);
                    break; // Stop after finding the first target
                }
                 /* else
                {
                    Debug.DrawRay(transform.position, direction * hit.distance, Color.green);
                }*/
                else
                  {
                  //transform.rotation = target;
                  Debug.DrawRay(transform.position, direction * radius, Color.blue);
                     }
            }
           
           


        }
        positionsList.Clear();
    }

    private Vector3 GetMeanVector(Vector3[] positions)
    {
        if (positions.Length == 0)
            return Vector3.zero;
        float x = 0f;
        float y = 0f;
        float z = 0f;
        foreach (Vector3 pos in positions)
        {
            x += pos.x;
            y += pos.y;
            z += pos.z;
        }
        return new Vector3(x / positions.Length, y / positions.Length, z / positions.Length);
    }

}
