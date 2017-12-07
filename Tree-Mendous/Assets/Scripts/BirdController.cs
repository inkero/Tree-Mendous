using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour {

	Rigidbody2D myRB;
	bool facingRight;
	public float move;
	public float speed;

	public float verticalDir;
	public float forceCount = 0;
	public float maxForceCount = 130;
	float maxForceCountDoubled;

	// Use this for initialization
	void Start () {
		myRB = GetComponent<Rigidbody2D> ();
		facingRight = false;

		verticalDir = 1;
		maxForceCountDoubled = maxForceCount * 2;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Move the character
		myRB.velocity = new Vector2 (move * speed, myRB.velocity.y);

		if(verticalDir == 1){
			myRB.AddForce(transform.up);
		} else if(verticalDir == -1){
			myRB.AddForce(-transform.up);
		}

		forceCount++;
		
		if(forceCount >= maxForceCount){
			forceCount = 0;
			verticalDir = -verticalDir;
			maxForceCount = maxForceCountDoubled;
		}
	}
}
