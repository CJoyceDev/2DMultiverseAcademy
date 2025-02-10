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
    /*    enum PlayerState
        {
            Grounded,
            Jumping,
            Falling,
            Knockback,
            Ability1,
            Ability2
        }
        PlayerState _currentState;*/

    StateMachine _stateMachine = new StateMachine();

    GroundedState _groundedState = new GroundedState();
    JumpingState _jumpingState = new JumpingState();
    FallingState _fallingState = new FallingState();
    KnockbackState _knockbackState = new KnockbackState();
    Ability1State _ability1State = new Ability1State();
    Ability2State _ability2State = new Ability2State();



    private void Awake()
    {
        _flipped = false;
        /*_currentState = PlayerState.Grounded;*/
        _stateMachine.ChangeState(_groundedState);
    }

    private void Update()
    {
        transform.position += new Vector3(InputHandler.MovementDir.x * MoveSpeed * Time.deltaTime, InputHandler.MovementDir.y * MoveSpeed * Time.deltaTime);

        /*if (InputHandler.JumpPressed)
        {
            _stateMachine.ChangeState(_jumpingState);
        }

        switch (_stateMachine.currentState)
        {
            case var value when value == _groundedState:
                DoIdle();
                break;
            case var value when value == _jumpingState:
                DoJump();
                break;
            case var value when value == _fallingState:
                DoFall();
                break;
            case var value when value == _knockbackState:
                DoKnockback();
                break;
            case var value when value == _ability1State:
                DoAbility1();
                break;
            case var value when value == _ability2State:
                DoAbility2();
                break;
            default:
                break;
        }*/

    }

    private void FixedUpdate()
    {
        _moveVelocity.x = InputHandler.MovementDir.x * MoveSpeed;

    }

    void DoIdle()
    {
        if (InputHandler.JumpPressed)
        {
            _stateMachine.ChangeState(_jumpingState);
        }
        else if (!_isGrounded)
        {
            _stateMachine.ChangeState(_fallingState);
        }
    }
    void DoJump()
    {
        
    }
    void DoFall()
    {
      /*  if (_isGrounded)
        {
            _currentState = PlayerState.Grounded;
        }

        _moveVelocity.y = -FallFallForce;*/

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

