using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePadTrigger : MonoBehaviour
{

    [SerializeField] BouncePad bp;
    
    PlayerController playerController;
    Animator animator;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponentInChildren<Animator>();
    }


    //change jump force on enter and exit //PD
    void OnTriggerEnter(Collider colider)
    {

        if (colider.CompareTag("Player"))
        {
            colider.GetComponent<PlayerController>().jumpSpeed = bp.bouncePadForce;
            //animator.SetBool("PlayerBounce", false);



        }



    }
    void OnTriggerExit(Collider colider)
    {

        if (colider.CompareTag("Player"))
        {

            //animator.SetBool("PlayerBounce", true);
            animator.Play("Base Layer.Bounce");
           
            colider.GetComponent<PlayerController>().jumpSpeed = 5;
        }

    }
}
