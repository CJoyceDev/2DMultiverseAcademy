using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    [SerializeField] CapsuleCollider _collider;

    //move values
    public float MoveSpeed = 30f;
    public float JumpForce = 9f;
    public float JumpFallForce = 9f;
    public float FallFallForce = 12f;

    //move stuff
    Vector2 _moveVelocity;
    bool _jump, _jumpHeld, _jumpReleased;
    bool _flipped; //facing right off, left is on
    bool _canMove;

    //colision stuff
    RaycastHit _groundHit;
    RaycastHit _ceelingHit;
    bool _isGrounded;
    bool _isCeeling;

    //state stuff
    enum PlayerState
    {
        Grounded,
        Jumping,
        Falling,
        Knockback,
        Ability1,
        Ability2
    }
    PlayerState _currentState;
    

    private void Awake()
    {
        _flipped = false;
        _currentState = PlayerState.Grounded;
    }

    private void Update()
    {
        switch (_currentState)
        {
            case PlayerState.Grounded:
                DoIdle();
                break;
            case PlayerState.Jumping:
                DoJump();
                break;
            case PlayerState.Falling:
                DoFall();
                break;
            case PlayerState.Knockback:
                DoKnockback();
                break;
            case PlayerState.Ability1:
                DoAbility1();
                break;
            case PlayerState.Ability2:
                DoAbility2();
                break;
            default:
                break;
        }
        
    }

    private void FixedUpdate()
    {
        _moveVelocity.x = InputHandler.MovementDir.x * MoveSpeed;

    }

    void DoIdle()
    {
        if (InputHandler.JumpPressed)
        {
            _currentState = PlayerState.Jumping;
        }
        else if (!_isGrounded)
        {
            _currentState = PlayerState.Falling;
        }
    }
    void DoJump()
    {
        if (_isCeeling)
        {
            _isCeeling = false;
            _currentState = PlayerState.Falling;
        }
        else if (InputHandler.JumpReleased)
        {
            _currentState = PlayerState.Falling;
        }

        _moveVelocity.y = -JumpFallForce;



        if (_moveVelocity.y < 0)
        {
            _currentState = PlayerState.Falling;
        }

    }
    void DoFall()
    {
        if (_isGrounded)
        {
            _currentState = PlayerState.Grounded;
        }

        _moveVelocity.y = -FallFallForce;

    }
    void DoKnockback()
    {

    }
    void DoAbility1()
    {

    }
    void DoAbility2()
    {

    }

}

