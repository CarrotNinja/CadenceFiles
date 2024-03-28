using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleDecide : BaseState
{
    private BossSM sm;
    public int counter;


    public IdleDecide(BossSM stm) : base("decide", stm) {
        sm = (BossSM) this.stateMachine;
    }
    public override void Enter()
    {
        base.Enter();
        counter = 240;
    }

    public override void StateLogic()
    {
        if (counter < 0)
        {
            base.StateLogic();
            if (Vector2.Distance(sm.player.transform.position, sm.rigidBody.position) < sm.decideDist)
            {
                sm.ChangeState(sm.jump);
            }
            else
            {
                sm.ChangeState(sm.roll);
            }
        }
        
    }
    public override void Exit() { 
        base.Exit();
    }

    public override void Action()
    {
        base.Action();
        counter--;
    }
}
