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

        
        if (lumberjack.slashing)
        {
            //Slash();
            lumberjack.Attack = true;
            lumberjack.MyAnimator.SetBool("attack", true);
        }
        else { 
            lumberjack.ChangeState(new RangedState());
            lumberjack.Attack = false;
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
