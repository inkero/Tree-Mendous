using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorjackPatrolState : IMotorjackState
{

    private Motorjack motorjack;

    private float patrolTimer;

    private float patrolDuration = 4;

    public void Enter(Motorjack motorjack)
    {
        this.motorjack = motorjack;
    }

    public void Execute()
    {
        //Debug.Log("Patrolling");

        Patrol();

        if(motorjack.Target != null && motorjack.slashing)
        {
            motorjack.ChangeState(new MotorjackMeleeState());
        }
        else if (motorjack.Target != null)
        {
            motorjack.ChangeState(new MotorjackRangedState());
        }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {
        if (other.tag == "Edge")
        {
            motorjack.Target = null;
            //MotorjackFov enemySight = gameObject.GetComponentInChildren<MotorjackFov>();
            //blinded = true;
            motorjack.ChangeDirection();
        }
    }

    private void Patrol()
    {

        motorjack.Move();

        patrolTimer += Time.deltaTime;

        if (patrolTimer >= patrolDuration)
        {
            motorjack.ChangeState(new MotorjackIdleState());
        }
    }
}
