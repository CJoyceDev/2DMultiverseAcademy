using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZone : MonoBehaviour
{

    //no clue, something about memory allocation >:) //PD
    [SerializeField] bool ChangeCamPosition, ChangeCamOffset;
    [SerializeField] Vector3 CameraPosition, CameraOffset;


    CamFollowPlayer camFollowPlaer;

    //get camera object //PD
    private void Awake()
    {
        camFollowPlaer = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamFollowPlayer>();
    }

    //Check if the player did the trigger, and do the function based on declared numbers in the inspector //PD
    void OnTriggerEnter(Collider colider)
    {

        if (colider.CompareTag("Player"))
        {
            camFollowPlaer.changeCamera(ChangeCamPosition, CameraPosition, ChangeCamOffset, CameraOffset);
        }


    }
    //reset camera when leaving //PD
    void OnTriggerExit(Collider colider)
    {

        if (colider.CompareTag("Player"))
        {
            camFollowPlaer.changeCamera(false, Vector3.zero, false, Vector3.zero);
        }
    }


}
