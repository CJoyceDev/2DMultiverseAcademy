using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : IState
{

    void IState.Enter()
    {
        Debug.Log("entering Grounded state");
    }

    void IState.Execute()
    {
        Debug.Log("updating Grounded state");
    }

    void IState.Exit()
    {
        Debug.Log("exiting Grounded state");
    }
}
