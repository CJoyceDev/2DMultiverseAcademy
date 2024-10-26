using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookHazard : MonoBehaviour
{

    [SerializeField] MeshRenderer mr;
    [SerializeField] BoxCollider bc;
    [SerializeField] float timeToClose;

    public void StartBookHazard()
    {
        Debug.Log("A");
        StartCoroutine(CloseBook(timeToClose));
    }

    IEnumerator CloseBook(float time)
    {
        float timeTotal = 0;

        while(timeTotal < time)
        {
            timeTotal += Time.deltaTime;
            yield return null;
        }
        Debug.Log("B");
        mr.enabled = true;
        bc.enabled = true;
        StartCoroutine(ResetBook());

    }

    IEnumerator ResetBook()
    {
        float timeTotal = 0;

        while (timeTotal < .5f)
        {
            timeTotal += Time.deltaTime;
            yield return null;
        }
        Debug.Log("C");
        mr.enabled = false;
        bc.enabled = false;

    }


}
