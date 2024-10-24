using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimStateController : PlayerController
{
    //refrence to player controller
    PlayerController playerController = null;

    Animator animator = null;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.inputActions.Player.Move.IsPressed())
        {
            animator.SetBool("isMoving?", true);
        }
        else
        {
            animator.SetBool("isMoving?", false);
        }


    }


}
