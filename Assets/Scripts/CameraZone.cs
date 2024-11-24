using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZone : MonoBehaviour
{

    [SerializeField] GameObject camSettings;


    //Check if the player did the trigger, and do the stuff //PD
    void OnTriggerEnter(Collider colider)
    {

        if (colider.CompareTag("Player"))
        {
            camSettings.SetActive(true);
        }


    }
    //reset camera when leaving //PD
    void OnTriggerExit(Collider colider)
    {

        if (colider.CompareTag("Player"))
        {
            camSettings.SetActive(false);
        }
    }


}
