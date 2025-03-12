using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    //Animator holder 
    Animator _animator;
    //AnimationPlaying
    string _currentAnimation, _currentAnimation2;

    // Some Animation Conditions
    bool _hasLanded = false, _hasJumped = false, _hasSwapped = false, _hasAttacked = false;

    //Models With Animations
    [SerializeField] GameObject MaxObject;
    [SerializeField] GameObject EvieObject;

    //Squash Stretch Empty Target
    [SerializeField] Animator Squishy;


    //Refrence to main player script
    [SerializeField] CJMovementWithRB ps; //Ps for Player Script //PD


    void Start()
    {
        _animator = MaxObject.GetComponent<Animator>();
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
            _animator = EvieObject.GetComponent<Animator>();
            //Attack/Ability Animation
            if (!_hasAttacked)
            {
                _hasAttacked = true;
                ChangeAnimationTo("Attacking");
                SquishySquash("NoSquash");
                Invoke("AttackFinish", 0.3f);
            }
            
            _hasSwapped = true;
        }
        else if ((InputHandler.Ability2Pressed || InputHandler.Ability2Held))
        {
            _animator = MaxObject.GetComponent<Animator>();
            //Attack/Ability Animation
            if (!_hasAttacked)
            {
                _hasAttacked = true;
                ChangeAnimationTo("Attacking");
                SquishySquash("NoSquash");
                Invoke("AttackFinish", 0.3f);
            }

            _hasSwapped = true;
        }
    }

    void AnimationLogic()
    {
        if (!_hasAttacked)
        {
            if (ps.isGrounded)
            {
                if (!_hasLanded & !_hasJumped)
                {
                    ChangeAnimationTo("Landing");
                    SquishySquash("Squash");
                    Invoke("LandedFinish", 0.1f);
                }
                else if (InputHandler.moveHeld)
                {
                    ChangeAnimationTo("Walk");
                    SquishySquash("NoSquash");
                }
                else
                {
                    ChangeAnimationTo("Idle 0");
                    SquishySquash("NoSquash");
                }

                if (InputHandler.JumpHeld & !_hasJumped)
                {
                    ChangeAnimationTo("Jumping");
                    SquishySquash("Squish");
                    _hasJumped = true;
                    _hasLanded = false;
                }
            }
            else
            {
                if (ps.rb.velocity.y <= -0.1f)
                {
                    ChangeAnimationTo("Falling");
                    SquishySquash("NoSquash");
                    _hasJumped = false;
                    _hasLanded = false;
                }
                if (ps.rb.velocity.y >= 0.1f & _hasJumped /*& charSwapped*/)
                {
                    ChangeAnimationTo("Jumping");
                    SquishySquash("Squish");
                }
            }

            
        }
        

    }

    void LandedFinish()
    {
        _hasLanded = true;
    }

    void AttackFinish()
    {
        _hasAttacked = false;
    }

    //Foce Animation change Function
    void ChangeAnimationTo(string newAnimation)
    {
        //A backdoor of some kind for the stop ahead to allow animations to continue playing when the animator target was swapped
        if (_hasSwapped)
        {
            _animator.Play(newAnimation);

            _currentAnimation = newAnimation;

            _hasSwapped = false;
        }
        //stop animations from trying to start every few seconds and let them play //PD
        if (_currentAnimation == newAnimation) return;

        _animator.Play(newAnimation);

        _currentAnimation = newAnimation;
    }

    void SquishySquash(string newAnimation)
    {

        //stop animations from trying to start every few seconds and let them play //PD
        if (_currentAnimation2 == newAnimation) return;
        Squishy.Play(newAnimation);

        _currentAnimation2 = newAnimation;

    }
}
