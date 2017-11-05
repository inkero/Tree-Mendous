using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunbeamController : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Enemy") {
			Physics2D.IgnoreCollision (other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
		}
	}
}
