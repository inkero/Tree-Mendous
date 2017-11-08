﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour {

	[SerializeField]
	private LumberjackController enemy;
	float timer = 0;
	float hitPeriod = 1f;

	public AudioClip hitSound;
	public float hitVolume = 0.7f;

	void OnTriggerStay2D(Collider2D other){
		if(other.tag == "Player"){
			PlayerController player = other.gameObject.GetComponent<PlayerController>();
			

			timer = timer + 1 * Time.deltaTime;

			if(timer >= hitPeriod){
				timer = 0;
				player.addDamage (enemy.weaponDamage);
				transform.root.GetComponent<AudioSource>().PlayOneShot (hitSound, hitVolume);
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Player"){
			
		}
	}

}
