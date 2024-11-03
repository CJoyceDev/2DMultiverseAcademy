using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookHazardTrigger : MonoBehaviour
{

    [SerializeField] BookHazard bookHazard;
    bool bookOpen = true;

    //Check for Trigger bla bla if player Do stuff //PD
    void OnTriggerEnter(Collider colider)
    {

        if (colider.CompareTag("Player") && bookOpen)
        {
            //Start Book Closing
            bookHazard.StartBookHazard();
        }


    }
}
