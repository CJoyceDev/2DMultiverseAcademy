using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheckPoint : MonoBehaviour
{
    public Transform flagTransform;
    public GameObject CheckPointPrefab;

    Checkpoint checkpoint;

    private float topOfPole;

   //This script activates the flag object and moves it to the top of the pole.
   //Shouldnt need touched CJ

    Rigidbody rb;
    private bool checkPointActive;
   // Vector3 topOfPole;
    //Vector3 topOfPoleY;

    private void Awake()
    {
        topOfPole = flagTransform.position.y + 2f;
        rb = GetComponentInChildren<Rigidbody>();
        //Temp transform is used to stop the player spawing in the air CJ
        Vector3 tempTransform = new Vector3(0, 1f, 0);
        checkpoint = new Checkpoint(transform.position + tempTransform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointStore.instance.AddCheckpoint(checkpoint);

            /*CoinStore.CoinStorage.CheckPointSet();*/
            flagTransform.gameObject.SetActive(true);
            checkPointActive = true;
            
            
            print(flagTransform.position.y);
            print(topOfPole);
        }
    }

    private void FixedUpdate()
    {
        float moveSpeed = 2;
        float distance = moveSpeed * Time.deltaTime;
       


        //sets the movement vector equal to the distance multiplied by the input axis value. negative for moving left, 0 for standing still and positve for moving right CJ
        Vector3 movement = new Vector3(0f, distance, 0f);


        Vector3 curPosition = flagTransform.position;

        Vector3 newPosition;

        //this. CJ
        if (checkPointActive == true && topOfPole > flagTransform.position.y)
        {
            newPosition = curPosition + movement;
            flagTransform.position = newPosition;
            print(flagTransform.position.y);
        }
    }
}
