using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class JumpAttack : BaseState
{
    public BossSM sm;
    public int dir;
    public JumpAttack(StateMachine stm) : base("Jumping", stm) {
        sm = (BossSM)this.stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        if(sm.player.transform.position.x - sm.rigidBody.position.x > 0)
        {
            dir = 1;
        }
        else
        {
            dir = -1;
        }
        Vector2 v = sm.rigidBody.velocity;
        v.y += sm.jumpAttackHeight;
        sm.rigidBody.velocity = v;
    }
    public override void StateLogic()
    {
        base.StateLogic();
        if(sm.rigidBody.velocity.x * dir <= 0.1)
        {
            sm.ChangeState(sm.idle);
        }
    }
    public override void Action()
    {
        base.Action();
        Vector2 v = sm.rigidBody.velocity;
        v.x = dir * sm.rollSpeed * 0.5f;
        sm.rigidBody.velocity = v;
    }
   
    public override void Exit()
    {
        base.Exit();
        sm.rigidBody.velocity = new Vector2(0, 3);
    }
    
}
