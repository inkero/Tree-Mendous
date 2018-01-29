using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : ILumberjackState
{
    private Lumberjack lumberjack;

    private float throwTimer;
    private float throwCoolDown = 1.5f;
    private bool canThrow;

    public void Enter(Lumberjack lumberjack)
    {
        this.lumberjack = lumberjack;
    }

    public void Execute()
    {
        ThrowAxe();

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

    private void ThrowAxe()
    {
        throwTimer += Time.deltaTime;

        if(throwTimer >= throwCoolDown)
        {
            canThrow = true;
            throwTimer = 0;
        }

        if (canThrow)
        {
            canThrow = false;
            lumberjack.MyAnimator.SetBool("throw", true);
        }
    }
}
