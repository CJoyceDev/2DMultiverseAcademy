using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookHazardTrigger : MonoBehaviour
{

    [SerializeField] BookHazard bookHazard;
    
    //Check for Trigger bla bla if player Do stuff //PD
    void OnTriggerEnter(Collider colider)
    {

        if (colider.CompareTag("Player"))
        {
            //Start Book Closing
            bookHazard.StartBookHazard();
        }


    }
}
