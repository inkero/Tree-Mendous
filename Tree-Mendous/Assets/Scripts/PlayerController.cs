using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Movement variables
	public float maxSpeed;

	Rigidbody2D myRB;
	Animator myAnim;
	bool facingRight;
	public float myWidth;

	// Use this for initialization
	void Start () {
		myRB = GetComponent<Rigidbody2D> ();
		myAnim = GetComponent<Animator> ();
		myWidth = GetComponent<SpriteRenderer> ().bounds.extents.x;

		facingRight = true;
	}
	
	// Update is called every period of time
	void FixedUpdate () {
		float move = Input.GetAxis ("Horizontal");
		myAnim.SetFloat ("speed", Mathf.Abs(move));

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
		Vector3 theScale = transform.localScale;
		theScale.x *= -1; // Same as: theScale.x = theScale.x * -1
		transform.localScale = theScale;
	}
}
