using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorjackRangedState : IMotorjackState
{
    private Motorjack motorjack;

    private float throwTimer;
    private float throwCoolDown = 1.5f;
    private bool canThrow;

    public void Enter(Motorjack motorjack)
    {
        this.motorjack = motorjack;
        motorjack.movementSpeed = motorjack.chargeSpeed;
        motorjack.GetComponent<Animator>().speed = 1.5f;
    }

    public void Execute()
    {
        ThrowAxe();

        if(motorjack.Target != null && !motorjack.slashing)
        {
            motorjack.Move();
        }
        else
        {
            motorjack.ChangeState(new MotorjackIdleState());
        }
    }

    public void Exit()
    {
        motorjack.movementSpeed = motorjack.originalMovementSpeed;
        motorjack.GetComponent<Animator>().speed = 1f;
    }

    public void OnTriggerEnter(Collider2D other)
    {
        if(other.tag == "Player")
        {
            motorjack.Slash();
        }
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
            motorjack.MyAnimator.SetBool("throw", true);
        }
    }
}
