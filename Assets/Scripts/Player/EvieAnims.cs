using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvieAnims : MonoBehaviour
{
    //refrence to player controller
    PlayerController playerController;
    Rigidbody rb;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        rb = GetComponentInParent<Rigidbody>();
        if (playerController.inputActions.Player.Move.IsPressed())
        {
            animator.SetBool("isMoving?", true);
        }
        else
        {
            animator.SetBool("isMoving?", false);
        }

        if (playerController.inputActions.Player.Jump.IsPressed())
        {
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }


        if (rb.velocity.y < 0f)
        {

            animator.SetBool("isFalling", true);
            

        }
        else
        {
            animator.SetBool("isFalling", false);
            animator.SetBool("isGrounded", true);
            //print("true");
        }
        

    }



}