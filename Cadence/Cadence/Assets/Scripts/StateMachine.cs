using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class StateMachine : MonoBehaviour
{
    public BaseState CurrentState;

    public bool NotEmptyState() {
        return this.CurrentState!= null;
    }


    void Start()
    {
        this.CurrentState = this.GetFirstState();
        if(this.NotEmptyState())
        {
            this.CurrentState.Enter();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.NotEmptyState())
        {
            this.CurrentState.StateLogic();
        }
    }

    void LateUpdate()
    {
        if (this.NotEmptyState())
        {
            this.CurrentState.Action();
        }
    }

    public void ChangeState(BaseState bs)
    {
        this.CurrentState.Exit();

        this.CurrentState = bs;
        this.CurrentState.Enter();
    }

    protected virtual BaseState GetFirstState() {
        return null;
    }
}
