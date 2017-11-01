using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

	public float projectileSpeed;

	Rigidbody2D myRB;

	// Use this for initialization
	void Awake () {
		myRB = GetComponent<Rigidbody2D> ();

		// If the projectile is facing left, add force to the left  
		if (transform.localRotation.z > 0) {
			myRB.AddForce (new Vector2 (-1, 0) * projectileSpeed, ForceMode2D.Impulse);
		// Otherwise add force to right
		} else {
			myRB.AddForce (new Vector2 (1, 0) * projectileSpeed, ForceMode2D.Impulse);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.layer == 9) {
			Destroy (col.gameObject);
			Destroy (gameObject);
		} else if(col.gameObject.layer != 8) {
			Destroy (gameObject);
		}
	}
}
