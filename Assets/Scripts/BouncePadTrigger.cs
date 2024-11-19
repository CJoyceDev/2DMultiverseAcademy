using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePadTrigger : MonoBehaviour
{

    [SerializeField] BouncePad bp;


    //change jump force on enter and exit //PD
    void OnTriggerEnter(Collider colider)
    {

        if (colider.CompareTag("Player"))
        {
            colider.GetComponent<PlayerController>().jumpSpeed = bp.bouncePadForce;
        }



    }
    void OnTriggerExit(Collider colider)
    {

        if (colider.CompareTag("Player"))
        {
            colider.GetComponent<PlayerController>().jumpSpeed = 5;
        }

    }
}
