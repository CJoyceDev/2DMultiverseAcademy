using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblingPlatformTrigger : MonoBehaviour
{
    [SerializeField] CrumblingPlatform crumblingPlatform;
    [SerializeField] GameObject testTubeLeft;
    [SerializeField] GameObject testTubeRight;
    [SerializeField] BoxCollider hitBox;
 


    //check if something triggered the colider and if that something happens to be the player do stuff //PD
    void OnTriggerEnter(Collider colider)
    {

        if (colider.CompareTag("Player"))
        {
            //Start Book Closing
            crumblingPlatform.StartCrumbling();
            //added to stop the coreoutine being trggered multiple times CJ
            hitBox.enabled = false;

            if (testTubeLeft != null)
            {
               
                ShakeScript shakeScript = testTubeLeft.GetComponent<ShakeScript>();

                if (shakeScript != null)
                {
                    shakeScript.isActivated = true; 
                }
            }

            if (testTubeRight != null)
            {

                ShakeScript shakeScript = testTubeRight.GetComponent<ShakeScript>();

                if (shakeScript != null)
                {
                    shakeScript.isActivated = true;
                }
            }
        }


    }
}
