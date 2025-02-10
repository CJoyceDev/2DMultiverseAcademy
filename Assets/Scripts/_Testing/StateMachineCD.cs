using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IState
{
    public void Enter();
    public void Execute();
    public void Exit();
}

public class StateMachine
{
    public IState currentState;

    public void ChangeState(IState newState)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = newState;
        currentState.Enter();
    }

    public void Update()
    {
        if (currentState != null) currentState.Execute();
    }
}

public class TestState : IState
{
    StateMachineCD owner;

    public TestState(StateMachineCD owner) { this.owner = owner; }

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



public class StateMachineCD : MonoBehaviour
{
    StateMachine stateMachine = new StateMachine();

    void Start()
    {
        stateMachine.ChangeState(new TestState(this));
    }

    void Update()
    {
        stateMachine.Update();
    }
}
