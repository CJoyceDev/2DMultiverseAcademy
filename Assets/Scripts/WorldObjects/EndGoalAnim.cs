using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoalAnim : MonoBehaviour
{
    public float bobSpeed = 2f;      
    public float bobHeight = 0.5f;   
    public float rotateSpeed = 30f;  

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {

        float newY = startPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);


    }
}
