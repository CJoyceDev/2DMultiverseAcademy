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
    bool _hasLanded = false, _hasJumped = false, _hasSwapped = false, _hasAttacked = false, _hasKnockback = false;

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

    private void OnEnable()
    {

        _hasLanded = false;
        _hasJumped = false;
        _hasSwapped = false;
        _hasAttacked = false; 
        _hasKnockback = false;
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
                /*ps.PlaySound(ps.GrappleSound);*/
                SoundHandler.instance.PlaySound(ps.GrappleSound, transform, 1f);

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
                /*ps.PlaySound(ps.SlingSound);*/
                SoundHandler.instance.PlaySound(ps.SlingSound, transform, 1f);
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
                    if(!_hasKnockback) SquishySquash("Squash");
                    Invoke("LandedFinish", 0.1f);
                }
                else if (InputHandler.moveHeld)
                {
                    ChangeAnimationTo("Walk");
                    if (!_hasKnockback) SquishySquash("NoSquash");
                }
                else
                {
                    ChangeAnimationTo("Idle 0");
                    if (!_hasKnockback) SquishySquash("NoSquash");
                }

                if (InputHandler.JumpHeld & !_hasJumped)
                {
                    ChangeAnimationTo("Jumping");
                    if (!_hasKnockback) SquishySquash("Squish");
                    _hasJumped = true;
                    _hasLanded = false;
                }
            }
            else
            {
                if (ps.rb.velocity.y <= -0.1f)
                {
                    ChangeAnimationTo("Falling");
                    if (!_hasKnockback) SquishySquash("NoSquash");
                    _hasJumped = false;
                    _hasLanded = false;
                }
                if (ps.rb.velocity.y >= 0.1f & _hasJumped /*& charSwapped*/)
                {
                    ChangeAnimationTo("Jumping");
                    if (!_hasKnockback) SquishySquash("Squish");
                }
            }

            
        }


    }

    public void KB()
    {
        SquishySquash("Stretch");
        _hasKnockback = true;
        Invoke("KnockbackFinish", 0.2f);

    }

    void LandedFinish()
    {
        _hasLanded = true;
    }

    void AttackFinish()
    {
        _hasAttacked = false;
    }

    void KnockbackFinish()
    {
        _hasKnockback = false;
    }

    //Foce Animation change Function
   public void ChangeAnimationTo(string newAnimation)
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
        //A backdoor of another kind to not allow animations to swap from knockback on the squishSquash animator
        if (_hasKnockback) return;

        //stop animations from trying to start every few seconds and let them play //PD
        if (_currentAnimation2 == newAnimation) return;
        Squishy.Play(newAnimation);

        _currentAnimation2 = newAnimation;

    }
}
