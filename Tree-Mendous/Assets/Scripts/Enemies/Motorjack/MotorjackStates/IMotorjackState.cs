using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMotorjackState {

    void Execute();
    void Enter(Motorjack motorjack);
    void Exit();
    void OnTriggerEnter(Collider2D other);
	
}
