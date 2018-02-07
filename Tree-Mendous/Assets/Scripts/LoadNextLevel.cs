using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour {

	public int LevelSelect;

	void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player")
		    SceneManager.LoadScene(LevelSelect);
	}
}
