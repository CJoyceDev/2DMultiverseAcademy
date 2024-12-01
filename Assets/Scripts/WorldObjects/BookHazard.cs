using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookHazard : MonoBehaviour
{

    [SerializeField] MeshRenderer mr;
    [SerializeField] BoxCollider bc;
    [SerializeField] float timeToClose;

    bool bookOpen = true;

    //Start timer to end you //PD
    public void StartBookHazard()
    {
        if (bookOpen)
        {
            Debug.Log("A");
            StartCoroutine(CloseBook(timeToClose));
        }
        
    }
    //After timer show trigger colider that kills the player //PD
    IEnumerator CloseBook(float time)
    {
        bookOpen = false;
        //Play Animation Here //PD

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
    //resets the book to be open and ready to flatten you once more //PD
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
        bookOpen = true;

    }


}
