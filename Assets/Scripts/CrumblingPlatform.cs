using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblingPlatform : MonoBehaviour
{
    [SerializeField] MeshRenderer mr, mr2;
    [SerializeField] BoxCollider bc, bc2;
    [SerializeField] float timeToCrumble, recoverTime;

    public void StartCrumbling()
    {
        Debug.Log("A");
        StartCoroutine(Crumble(timeToCrumble));
    }

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
