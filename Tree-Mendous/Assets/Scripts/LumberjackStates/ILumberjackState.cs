using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILumberjackState {

    void Execute();
    void Enter(Lumberjack lumberjack);
    void Exit();
    void OnTriggerEnter(Collider2D other);
	
}
