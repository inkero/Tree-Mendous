using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowRoots : MonoBehaviour {

	public float maxHeight = 1;
	public float growSpeed = 2f;
	public float lifeTimer = 0;
	public float lifeTime = 10;

	bool growing = true;

	// Use this for initialization
	void Awake () {
		transform.localScale = new Vector3(0, 0, transform.localScale.z);
	}
	
	// Update is called once per frame
	void Update () {
		Physics2D.IgnoreLayerCollision (11, 11);
		Physics2D.IgnoreLayerCollision (8, 11);

		if (growing) {
			if (transform.localScale.y < maxHeight) {
				Vector3 newScale = transform.localScale;
				newScale.y += growSpeed * Time.deltaTime;
				newScale.x += growSpeed * Time.deltaTime;
				transform.localScale = newScale;
			} else {
				transform.localScale = new Vector3 (1, 1, transform.localScale.z);
				growing = false;
			}
		}

		if (lifeTimer > lifeTime) {
			Vector3 newNewScale = transform.localScale;
			newNewScale.y -= growSpeed * Time.deltaTime;
			transform.localScale = newNewScale;
		} 

		if (transform.localScale.y < 0) {
			Destroy (gameObject);
		}

		lifeTimer += 1 * Time.deltaTime;
	}
}
