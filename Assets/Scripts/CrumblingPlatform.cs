using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblingPlatform : MonoBehaviour
{
    [SerializeField] MeshRenderer mr, mr2;
    [SerializeField] BoxCollider bc, bc2;
    [SerializeField] float timeToCrumble, recoverTime;

    //start timer
    public void StartCrumbling()
    {
        Debug.Log("A");
        StartCoroutine(Crumble(timeToCrumble));
    }

    //at the end of the timer, hide the platform rendered and coliders, start timer to restart them //PD
    IEnumerator Crumble(float time)
    {
        float timeTotal = 0;

        while (timeTotal < time)
        {
            timeTotal += Time.deltaTime;
            yield return null;
        }
        Debug.Log("B");
        (mr.enabled, mr2.enabled) = (false, false);
        (bc.enabled, bc2.enabled) = (false, false);
        StartCoroutine(ResetPlatform(recoverTime));

    }
    //Restarts the platform to be visible and colidable after the timer //PD
    IEnumerator ResetPlatform(float time)
    {
        float timeTotal = 0;

        while (timeTotal < time)
        {
            timeTotal += Time.deltaTime;
            yield return null;
        }
        Debug.Log("C");

        (mr.enabled, mr2.enabled) = (true, true);
        (bc.enabled, bc2.enabled) = (true, true);

        

    }


}
