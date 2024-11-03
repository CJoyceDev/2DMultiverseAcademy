using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZone : MonoBehaviour
{


    [SerializeField] bool ChangeCamPosition, ChangeCamOffset;
    [SerializeField] Vector3 CameraPosition, CameraOffset;


    CamFollowPlayer camFollowPlaer;

    private void Awake()
    {
        camFollowPlaer = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamFollowPlayer>();
    }

    void OnTriggerEnter(Collider colider)
    {

        if (colider.CompareTag("Player"))
        {
            camFollowPlaer.changeCamera(ChangeCamPosition, CameraPosition, ChangeCamOffset, CameraOffset);
        }


    }

    void OnTriggerExit(Collider colider)
    {

        if (colider.CompareTag("Player"))
        {
            camFollowPlaer.changeCamera(false, Vector3.zero, false, Vector3.zero);
        }
    }


}
