using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability1State : IState
{

    void IState.Enter()
    {
        Debug.Log("entering test state");
    }

    void IState.Execute()
    {
        Debug.Log("updating test state");
    }

    void IState.Exit()
    {
        Debug.Log("exiting test state");
    }
}
