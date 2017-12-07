using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GroundEnemy : MonoBehaviour {

	public LayerMask enemyMask;
	public float maxSpeed;
	public float move;
	public float flipDelay;
	public float lineCastDown = .3f;

	public GameObject target;
	public GameObject pickup;

	public float passedWaitTime;
	Rigidbody2D myRB;
	Animator myAnim;
	float myWidth;
	float myHeight;
	bool facingRight;

	public float enemyMaxHealth;
	public float currentHealth;
	public float weaponDamage = 50;

	AudioSource audioSource;

	public AudioClip hitSound;
	public float hitVolume = 0.7f;
	public AudioClip deathSound;
	public float deathVolume = 0.7f;

	// Use this for initialization
	void Start () {
		myRB = GetComponent<Rigidbody2D> ();
		myAnim = GetComponent<Animator> ();
		myWidth = GetComponent<SkinnedMeshRenderer> ().bounds.extents.x;
		myHeight = GetComponent<SkinnedMeshRenderer> ().bounds.extents.y;
		audioSource = GetComponent<AudioSource> ();

		facingRight = false;

		//audioSource.clip = chainsawSound;
		//audioSource.Play ();

		currentHealth = enemyMaxHealth;
		audioSource = GetComponent<AudioSource> ();
	}

	void FixedUpdate () {
		if (target == null) {
			patrol ();
			myAnim.SetBool ("agression", false);
		} else if (target.gameObject.tag == "Player") {
			patrol ();
			myAnim.SetBool ("agression", true);
		}
	}

	/*
	void followPlayer(){
		if (target.transform.position.x < transform.position.x) {
			facingRight = false;
			move = -1;
		} else {
			facingRight = true;
			move = 1;
		}
	}
	*/

	void patrol(){
		myAnim.SetFloat ("speed", Mathf.Abs(move));

		// Check to see if there's ground in front of us before moving forward
		Vector2 lineCastPos = transform.position - transform.up * myHeight - transform.right * myWidth;
		Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down * lineCastDown);
		bool isGrounded = Physics2D.Linecast (lineCastPos, lineCastPos + Vector2.down * lineCastDown, enemyMask);

		// Check if there's a wall
		Vector2 lineCastPos2 = transform.position - transform.up * myHeight - transform.right * myWidth;
		Debug.DrawLine(lineCastPos2, lineCastPos2 - transform.right.toVector2() * .2f);
		bool isBlocked = Physics2D.Linecast (lineCastPos2, lineCastPos2 - transform.right.toVector2() * .2f, enemyMask);

		// If the linecast is not colliding with ground, that means we need to stop
		if (!isGrounded || isBlocked) {

			// Initially the passed time stays at zero, so we automatically start a wait timer
			if (passedWaitTime < flipDelay) {

				// Stop the character & start counting seconds for the passed wait time
				move = 0;
				passedWaitTime = passedWaitTime + Time.deltaTime;

				// When passed time reaches set delay, move to other direction and reset the timer
			} else {
				resetWalk ();
				passedWaitTime = 0;
			}
		}

		// If the AI is somehow moved into grounded mode while waiting, resume walking
		if (move == 0 && isGrounded) {
			resetWalk ();
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

	void resetWalk(){
		if (!facingRight) {
			move = 1;
		} else {
			move = -1;
		}
	}

	void flip(){
		facingRight = !facingRight;
		Vector3 enemyRotation = transform.eulerAngles;
		enemyRotation.y += 180; // Same as: theScale.x = theScale.x * -1
		transform.eulerAngles = enemyRotation;
	}

	public void addDamage(float damage){
		//myAnim.SetBool ("hit", true);
		currentHealth -= damage;
		audioSource.PlayOneShot (hitSound, hitVolume);

		if (currentHealth <= 0){
			StartCoroutine ("makeDead");
		}
	}


	IEnumerator makeDead(){
		myAnim.SetBool ("died", true);
		audioSource.PlayOneShot (deathSound, deathVolume);
		yield return new WaitForSeconds (0.5f);
		Instantiate (pickup, transform.position + new Vector3(0, -.5f, 0), Quaternion.Euler (new Vector3 (0, 0, 0)));
		Destroy (gameObject);
	}
}
