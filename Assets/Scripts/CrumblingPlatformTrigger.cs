using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblingPlatformTrigger : MonoBehaviour
{
    [SerializeField] CrumblingPlatform crumblingPlatform;
    

    void OnTriggerEnter(Collider colider)
    {

        if (colider.CompareTag("Player"))
        {
            //Start Book Closing
            crumblingPlatform.StartCrumbling();
        }


    }
}
