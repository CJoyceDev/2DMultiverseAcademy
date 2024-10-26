using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookHazardTrigger : MonoBehaviour
{

    [SerializeField] BookHazard bookHazard;
    bool bookOpen = true;

    void OnTriggerEnter(Collider colider)
    {

        if (colider.CompareTag("Player") && bookOpen)
        {
            //Start Book Closing
            bookHazard.StartBookHazard();
        }


    }
}
