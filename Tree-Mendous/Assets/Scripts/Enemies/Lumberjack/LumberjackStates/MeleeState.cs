using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeState : ILumberjackState
{
    private Lumberjack lumberjack;

    public void Enter(Lumberjack lumberjack)
    {
        this.lumberjack = lumberjack;
    }

    public void Execute()
    {
        if (lumberjack.Target != null)
        {
            lumberjack.Move();
        }
        else
        {
            lumberjack.ChangeState(new IdleState());
        }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
        
    }
}
