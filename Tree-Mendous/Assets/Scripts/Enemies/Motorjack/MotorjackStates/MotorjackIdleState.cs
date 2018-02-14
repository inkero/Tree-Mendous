using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorjackIdleState : IMotorjackState
{

    private Motorjack motorjack;

    private float idleTimer;

    private float idleDuration = 2;

    

    public void Enter(Motorjack motorjack)
    {
        this.motorjack = motorjack;
    }

    public void Execute()
    {
        //Debug.Log("Idling");

        Idle();

        if(motorjack.Target != null)
        {
            motorjack.ChangeState(new MotorjackPatrolState());
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
        motorjack.MyAnimator.SetFloat("speed", 0);

        idleTimer += Time.deltaTime;

        if(idleTimer >= idleDuration)
        {
            motorjack.ChangeState(new MotorjackPatrolState());
        }
    }
}
