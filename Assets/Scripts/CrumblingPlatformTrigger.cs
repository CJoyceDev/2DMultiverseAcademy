using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblingPlatformTrigger : MonoBehaviour
{
    [SerializeField] CrumblingPlatform crumblingPlatform;
    
    //check if something triggered the colider and if that something happens to be the player do stuff //PD
    void OnTriggerEnter(Collider colider)
    {

        if (colider.CompareTag("Player"))
        {
            //Start Book Closing
            crumblingPlatform.StartCrumbling();
        }


    }
}
