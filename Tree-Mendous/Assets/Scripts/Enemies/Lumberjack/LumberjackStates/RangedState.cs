﻿using System.Collections;
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
        lumberjack.movementSpeed = lumberjack.chargeSpeed;
        lumberjack.GetComponent<Animator>().speed = 1.5f;
    }

    public void Execute()
    {
        ThrowAxe();

        if(lumberjack.Target != null && !lumberjack.slashing)
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
        lumberjack.movementSpeed = lumberjack.originalMovementSpeed;
        lumberjack.GetComponent<Animator>().speed = 1f;
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
