using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour {

	void OnTriggerEnter2D(){
		SceneManager.LoadScene("Main Menu");
	}
}
