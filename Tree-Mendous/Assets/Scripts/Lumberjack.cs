using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumberjack : Character {

    private ILumberjackState currentState;

	// Use this for initialization
	public override void Start () {
        base.Start();
        facingRight = false;
        ChangeState(new IdleState());
	}
	
	// Update is called once per frame
	void Update () {
        currentState.Execute();
	}

    public void ChangeState(ILumberjackState newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);
    }

    public void Move()
    {
        MyAnimator.SetFloat("speed", 1);

        transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        currentState.OnTriggerEnter(other);
    }

}
