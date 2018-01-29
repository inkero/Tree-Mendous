using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : ILumberjackState
{

    private Lumberjack lumberjack;

    private float patrolTimer;

    private float patrolDuration = 10;

    public void Enter(Lumberjack lumberjack)
    {
        this.lumberjack = lumberjack;
    }

    public void Execute()
    {
        Debug.Log("Patrolling");

        Patrol();

        if(lumberjack.Target != null)
        {
            lumberjack.ChangeState(new RangedState());
        }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
        if(other.tag == "Edge")
        {
            lumberjack.ChangeDirection();
        }
    }

    private void Patrol()
    {

        patrolTimer += Time.deltaTime;

        lumberjack.Move();

        if (patrolTimer >= patrolDuration)
        {
            lumberjack.ChangeState(new IdleState());
        }
    }
}
