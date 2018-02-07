using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : ILumberjackState
{

    private Lumberjack lumberjack;

    private float idleTimer;

    private float idleDuration = 2;

    

    public void Enter(Lumberjack lumberjack)
    {
        this.lumberjack = lumberjack;
    }

    public void Execute()
    {
        //Debug.Log("Idling");

        Idle();

        if(lumberjack.Target != null)
        {
            lumberjack.ChangeState(new PatrolState());
        }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
         
    }

    private void Idle()
    {
        lumberjack.MyAnimator.SetFloat("speed", 0);

        idleTimer += Time.deltaTime;

        if(idleTimer >= idleDuration)
        {
            lumberjack.ChangeState(new PatrolState());
        }
    }
}
