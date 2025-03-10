using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    //Animator holder 
    Animator animator;
    //AnimationPlaying
    string currentAnimation;

    // Some Animation Conditions
    bool landed = false, jumping = false;

    //Models With Animations
    [SerializeField] GameObject MaxObject;
    [SerializeField] GameObject EvieObject;


    //Refrence to main player script
    [SerializeField] CJMovementWithRB ps; //Ps for Player Script //PD


    void Start()
    {
        animator = MaxObject.GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        SwapAnimator();
        AnimationLogic();
    }

    //Swap between Max and Evie animators
    void SwapAnimator()
    {
        //Player Model Animator Swap
        if ((InputHandler.Ability1Pressed || InputHandler.Ability1Held))
        {
            animator = EvieObject.GetComponent<Animator>();
        }
        else if ((InputHandler.Ability2Pressed || InputHandler.Ability2Held))
        {
            animator = MaxObject.GetComponent<Animator>();
        }
    }

    void AnimationLogic()
    {
        if (ps.isGrounded)
        {
            if (!landed & !jumping)
            {
                ChangeAnimationTo("Landing");
                Invoke("LandedFinish", 0.1f);
            }
            else if (InputHandler.moveHeld)
            {
                ChangeAnimationTo("Walk");
            }
            else
            {
                ChangeAnimationTo("Idle 0");
            }

            if (InputHandler.JumpHeld & !jumping)
            {
                ChangeAnimationTo("Jumping");
                jumping = true;
                landed = false;
            }
        }
        else if (ps.rb.velocity.y <= -0.1)
        {
            ChangeAnimationTo("Falling");
            jumping = false;
            landed = false;
        }

        
    }

    void LandedFinish()
    {
        landed = true;
    }

    //Foce Animation change Function
    void ChangeAnimationTo(string newAnimation)
    {
        //stop animations from trying to start every few seconds and let them play //PD
        if (currentAnimation == newAnimation) return;

        animator.Play(newAnimation);

        currentAnimation = newAnimation;
    }
}
