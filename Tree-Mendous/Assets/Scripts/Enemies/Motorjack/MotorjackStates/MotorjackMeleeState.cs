using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorjackMeleeState : IMotorjackState
{
    private Motorjack motorjack;

    public void Enter(Motorjack motorjack)
    {
        this.motorjack = motorjack;
    }

    public void Execute()
    {

        
        if (motorjack.slashing)
        {
            //Slash();
            motorjack.Attack = true;
            motorjack.MyAnimator.SetBool("attack", true);
        }
        else { 
            motorjack.ChangeState(new MotorjackRangedState());
            motorjack.Attack = false;
        }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D other)
    {

    }

    private void Slash()
    {
        Debug.Log("slash");

        //lumberjack.MyAnimator.SetFloat("speed", 0);

        

        

        //lumberjack.audioSource.PlayOneShot(lumberjack.hitSound, lumberjack.hitVolume);
    }
}
