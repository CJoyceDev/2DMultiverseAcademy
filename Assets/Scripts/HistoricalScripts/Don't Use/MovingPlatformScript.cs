using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    
    [SerializeField] Vector3 firstLocation, secondLocation;
    [SerializeField] float timeTotal = 1, timeTaken = 0;
    public Vector3 velocity;
    private Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = firstLocation;
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //check if time taken is less than the max/total time of the movement, increment time taken and check again while moving the object
        if (timeTaken < timeTotal)
        {
            float t = timeTaken / timeTotal;
            transform.position = Vector3.Lerp(firstLocation, secondLocation, t);
            timeTaken += Time.deltaTime;
        }
        else
        {
            //put object at last possible location, swap start and end, restart time
            transform.position = secondLocation;
            (firstLocation, secondLocation) = (secondLocation, firstLocation); //swaps the start and end points
            timeTaken = 0;
        }
        velocity = transform.position - lastPosition;
        lastPosition = transform.position;



    }

    void OnTriggerEnter(Collider other)
    {
        other.transform.SetParent(transform);
    }

    void OnTriggerExit(Collider other)
    {
        other.transform.SetParent(null);
    }




}
