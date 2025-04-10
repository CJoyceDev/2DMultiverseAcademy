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
    Vector3 ResetSpot;
    Vector3 localEulerAngles;
    public float minRotation = -45f;
    public float maxRotation = 45f;

    // Quaternion target = Quaternion.Euler(-10, 0, 0);
    void Start()
    {
        ResetSpot = new Vector3(-10, 90, 0);

    }

    void Update()
    {
        positionsList.Clear();
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

            if(positionsList.Count == 0)
            {
                transform.localRotation = Quaternion.Euler(ResetSpot);
            }

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

                    localEulerAngles = transform.localEulerAngles;
                    //Debug.Log(localEulerAngles);
                    if (localEulerAngles.y == 270)
                    {
                        transform.localRotation = Quaternion.Euler(ResetSpot);
                    }
                    if (localEulerAngles.y == 270 && localEulerAngles.x > 70 && localEulerAngles.x < 80 )
                    {
                        transform.LookAt(TargetSpot);
                    }


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
