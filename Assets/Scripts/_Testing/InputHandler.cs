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

    //moving
    public static Vector2 MovementDir;
    public static bool moveHeld;

    InputAction _move;
    InputAction _jump;
    /*InputAction _ability;*/


    private void Awake()
    {
        inputActions = GetComponent<PlayerInput>();
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

    }

}
