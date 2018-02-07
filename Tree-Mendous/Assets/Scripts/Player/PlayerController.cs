using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour {

	// Movement variables
	public float maxSpeed;

	// Jumping variables
	bool grounded = false;
	bool rootWallMuzzleGrounded = false;
	float groundCheckRadius = 0.2f;
	public LayerMask groundLayer;
	public Transform groundCheck;
	public float jumpHeight = 15f;
	public float move;
	public bool knockedBack = false;

	// For respawn
	public Transform spawnPoint;

	// For jump delay
	float passedWaitTime;
	float jumpDelay = 0.2f;
	bool startJumpTimer = false;

	Rigidbody2D myRB;
	Animator myAnim;
	bool facingRight;

	// For pickups
	public float maxPower = 100f;
	public float currentPower;

	public float maxHealth;
	public float currentHealth;

    //For HUD
    public GameObject heart1, heart2, heart3, heart4, energy1, energy2, energy3, energy4, energy5;

	// For shooting
	public Transform weaponMuzzle;
	public GameObject bullet;
	float fireRate = 0.5f;
	float nextFire = 0f;
	int fireCount = 6;

	// For casting root wall
	public Transform rootWallMuzzle;
	public GameObject rootWall;

    // For audio
	public AudioClip shootSound;
	public float shootVolume = 0.7f;
	public AudioClip rootWallSound;
	public float rootWallVolume = 0.3f;
	public AudioClip cheerSound;
	public float cheerVolume = 0.3f;
	public AudioClip jumpSound;
	public float jumpVolume = 0.7f;
	public AudioClip pickupSound;
	public float pickupVolume = 0.2f;
	public AudioClip hitSound;
	public float hitVolume = 0.7f;
	public AudioClip deathSound;
	public float deathVolume = 0.7f;
	AudioSource audioSource;



	// Use this for initialization
	void Start () {
		myRB = GetComponent<Rigidbody2D> ();
		myAnim = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource> ();

        currentHealth = maxHealth;
        heart1.gameObject.SetActive(true);
        heart2.gameObject.SetActive(true);
        heart3.gameObject.SetActive(true);
        heart4.gameObject.SetActive(true);
        energy1.gameObject.SetActive(true);
        energy2.gameObject.SetActive(true);
        energy3.gameObject.SetActive(true);
        energy4.gameObject.SetActive(true);
        energy5.gameObject.SetActive(true);

		facingRight = true;
	}

	void Update() {
		
		if (grounded && Input.GetAxisRaw ("Jump") > 0 && !startJumpTimer) {
			startJumpTimer = true; // Used for inconsistent jumping fix
			grounded = false;
			myAnim.SetBool ("isGrounded", grounded);
			myRB.AddForce (new Vector2 (0, jumpHeight), ForceMode2D.Impulse);
			audioSource.PlayOneShot (jumpSound, jumpVolume);
		}
        if (currentHealth == 100f)
        {
            heart1.gameObject.SetActive(true);
            heart2.gameObject.SetActive(true);
            heart3.gameObject.SetActive(true);
            heart4.gameObject.SetActive(true);
        }
        if (currentHealth == 75f)
        {
            heart1.gameObject.SetActive(true);
            heart2.gameObject.SetActive(true);
            heart3.gameObject.SetActive(true);
            heart4.gameObject.SetActive(false);
        }
        if (currentHealth == 50f)
        {
            heart1.gameObject.SetActive(true);
            heart2.gameObject.SetActive(true);
            heart3.gameObject.SetActive(false);
            heart4.gameObject.SetActive(false);
        }
        if (currentHealth == 25f)
        {
            heart1.gameObject.SetActive(true);
            heart2.gameObject.SetActive(false);
            heart3.gameObject.SetActive(false);
            heart4.gameObject.SetActive(false);
        }
        if (currentHealth == 0)
        {
            heart1.gameObject.SetActive(false);
            heart2.gameObject.SetActive(false);
            heart3.gameObject.SetActive(false);
            heart4.gameObject.SetActive(false);
        }

        if (currentPower == 100f)
        {
            energy1.gameObject.SetActive(true);
            energy2.gameObject.SetActive(true);
            energy3.gameObject.SetActive(true);
            energy4.gameObject.SetActive(true);
            energy5.gameObject.SetActive(true);
        }
        if (currentPower == 80f)
        {
            energy1.gameObject.SetActive(true);
            energy2.gameObject.SetActive(true);
            energy3.gameObject.SetActive(true);
            energy4.gameObject.SetActive(true);
            energy5.gameObject.SetActive(false);
        }
        if (currentPower == 60f)
        {
            energy1.gameObject.SetActive(true);
            energy2.gameObject.SetActive(true);
            energy3.gameObject.SetActive(true);
            energy4.gameObject.SetActive(false);
            energy5.gameObject.SetActive(false);
        }
        if (currentPower == 40f)
        {
            energy1.gameObject.SetActive(true);
            energy2.gameObject.SetActive(true);
            energy3.gameObject.SetActive(false);
            energy4.gameObject.SetActive(false);
            energy5.gameObject.SetActive(false);
        }
        if (currentPower == 20f)
        {
            energy1.gameObject.SetActive(true);
            energy2.gameObject.SetActive(false);
            energy3.gameObject.SetActive(false);
            energy4.gameObject.SetActive(false);
            energy5.gameObject.SetActive(false);
        }
        if (currentPower == 0)
        {
            energy1.gameObject.SetActive(false);
            energy2.gameObject.SetActive(false);
            energy3.gameObject.SetActive(false);
            energy4.gameObject.SetActive(false);
            energy5.gameObject.SetActive(false);
        }


        // Player shooting
        if (Input.GetAxisRaw ("Fire1") > 0) {
			fire ();
		}

		// Player shooting
		if (Input.GetAxisRaw ("Fire2") > 0 && grounded && rootWallMuzzleGrounded) {
			createRootWall ();
		}
	}

	// Update is called every period of time
	void FixedUpdate () {

		// To fix inconsistent jumping
		if (startJumpTimer) {
			if (passedWaitTime < jumpDelay) {
				passedWaitTime = passedWaitTime + Time.deltaTime;
			} else {
				passedWaitTime = 0;
				startJumpTimer = false;
			}
		}

		// Check if we are grounded - if no, then we are falling
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
		myAnim.SetBool ("isGrounded", grounded);

		// Check if the root wall muzzle isn't hovering in air
		rootWallMuzzleGrounded = Physics2D.OverlapCircle(rootWallMuzzle.position, groundCheckRadius, groundLayer);

		myAnim.SetFloat ("verticalSpeed", myRB.velocity.y);

		move = Input.GetAxis ("Horizontal");
		myAnim.SetFloat ("speed", Mathf.Abs(move));

		if(grounded){
			knockedBack = false;
		}

		// Move the character
		if(!knockedBack){
			myRB.velocity = new Vector2 (move * maxSpeed, myRB.velocity.y);
		}

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

	void fire(){
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			audioSource.pitch = 0.18f;
			audioSource.PlayOneShot (shootSound, shootVolume);
			myAnim.SetTrigger ("shoot");
			audioSource.pitch = 1f;
			if (facingRight) {
				Instantiate (bullet, weaponMuzzle.position, Quaternion.Euler (new Vector3 (0, 0, 0f)));
			} else if(!facingRight) {
				Instantiate (bullet, weaponMuzzle.position, Quaternion.Euler (new Vector3 (0, 0, 180f)));
			}

			fireCount++;

			if (fireCount >= 14) {
				audioSource.PlayOneShot (cheerSound, cheerVolume);
				fireCount = 0;
			}
		}
	}

	void createRootWall(){
        Debug.Log("Placing rootwall");
		if (Time.time > nextFire && currentPower > 0) {
			nextFire = Time.time + fireRate;
			audioSource.PlayOneShot (rootWallSound, rootWallVolume);
			Instantiate (rootWall, rootWallMuzzle.position, Quaternion.Euler (new Vector3 (0, 0, 0)));
			currentPower = currentPower - 20;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Waterbeam") {
			if (!(currentPower >= maxPower)) {
				currentPower += 20;
				audioSource.PlayOneShot (pickupSound, pickupVolume);
				Destroy (other.gameObject);
			}
        }
    }

    public void addDamage(float damage){
		//myAnim.SetBool ("hit", true);
		currentHealth -= damage;
		audioSource.PlayOneShot (hitSound, hitVolume);
		//myRB.AddForce (new Vector2 (0, 10f), ForceMode2D.Impulse);
		//float originalGS = myRB.gravityScale;
		//myRB.gravityScale = 0;

		myAnim.SetBool ("hit", true);

		knockedBack = true;
		myRB.velocity = new Vector2 (0, 5f);
		//myRB.gravityScale = originalGS;


		if (currentHealth <= 0){
			StartCoroutine ("makeDead");
		}
	}
    
	IEnumerator makeDead(){
		myAnim.SetBool ("died", true);
		yield return new WaitForSeconds (0.5f);
		//GameObject.FindWithTag ("MainCamera").GetComponent<AudioSource>().PlayOneShot (deathSound, deathVolume);
		GameObject.FindWithTag ("MainCamera").GetComponent<CustomRestarter>().StartCoroutine("Restart");


	}
}
