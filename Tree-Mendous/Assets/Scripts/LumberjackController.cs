using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumberjackController : MonoBehaviour {

	public LayerMask enemyMask;
	public float maxSpeed;
	public float move;
	public float flipDelay;

	float passedWaitTime;
	Rigidbody2D myRB;
	Animator myAnim;
	float myWidth;
	float myHeight;
	bool facingRight;

	// Use this for initialization
	void Start () {
		myRB = GetComponent<Rigidbody2D> ();
		myAnim = GetComponent<Animator> ();
		myWidth = GetComponent<SkinnedMeshRenderer> ().bounds.extents.x;
		myHeight = GetComponent<SkinnedMeshRenderer> ().bounds.extents.y;

		facingRight = false;
	}

	void FixedUpdate () {
		myAnim.SetFloat ("speed", Mathf.Abs(move));

		// Check to see if there's ground in front of us before moving forward
		Vector2 lineCastPos = transform.position - transform.up * myHeight - transform.right * myWidth;
		Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down*2);
		bool isGrounded = Physics2D.Linecast (lineCastPos, lineCastPos + Vector2.down*2, enemyMask);

		// If the linecast is not colliding with ground, that means we need to stop
		if (!isGrounded) {

			// Initially the passed time stays at zero, so we automatically start a wait timer
			if (passedWaitTime < flipDelay) {

				// Stop the character & start counting seconds for the passed wait time
				move = 0;
				passedWaitTime = passedWaitTime + Time.deltaTime;

			// When passed time reaches set delay, move to other direction and stop the timer
			} else {
				if (!facingRight) {
					move = 1;
				} else {
					move = -1;
				}
				passedWaitTime = 0;
			}
		}

		// Move the character
		myRB.velocity = new Vector2 (move * maxSpeed, myRB.velocity.y);

		// If we move right & the character is not facing right, flip
		if(move>0 && !facingRight){
			flip ();
			// And if we move left & the character is facing right, flip
		} else if(move<0 && facingRight) {
			flip ();
		}
	}


	void flip(){
		facingRight = !facingRight;
		Vector3 enemyRotation = transform.eulerAngles;
		enemyRotation.y += 180; // Same as: theScale.x = theScale.x * -1
		transform.eulerAngles = enemyRotation;
	}
}
