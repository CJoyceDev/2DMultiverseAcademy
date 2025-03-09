using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    //Figured out this little trick to NOT have input for every individual script but to yoink it from here //PD

    public static PlayerInput inputActions;

    //jumping
    public static bool JumpPressed;
    public static bool JumpHeld;
    public static bool JumpReleased;

    InputAction _jump;

    //moving
    public static Vector2 MovementDir;
    public static bool moveHeld;

    InputAction _move;


    //abilities
    public static bool Ability1Pressed;
    public static bool Ability1Held;
    public static bool Ability1Released;

    public static bool Ability2Pressed;
    public static bool Ability2Held;
    public static bool Ability2Released;

    InputAction _ability1;
    InputAction _ability2;


    private void Awake()
    {
        inputActions = GetComponent<PlayerInput>();
        _move = inputActions.actions["Move"]; //wasd
        _jump = inputActions.actions["Jump"]; //space
        _ability1 = inputActions.actions["Swap"]; //r
        _ability2 = inputActions.actions["Ability"]; //f
    }

    private void Update()
    {
        //For juming, tap or held or half way
        JumpPressed = _jump.WasPressedThisFrame();
        JumpHeld = _jump.IsPressed();
        JumpReleased = _jump.WasReleasedThisFrame();
        //For Moving, and triggering move state
        moveHeld = _move.IsPressed();
        MovementDir = _move.ReadValue<Vector2>();
        //For Abilities
        Ability1Pressed = _ability1.WasPressedThisFrame();
        Ability1Held = _ability1.IsPressed();
        Ability1Released = _ability1.WasReleasedThisFrame();

        Ability2Pressed = _ability2.WasPressedThisFrame();
        Ability2Held = _ability2.IsPressed();
        Ability2Released = _ability2.WasReleasedThisFrame();

    }

}
