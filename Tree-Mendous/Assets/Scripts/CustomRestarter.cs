using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRestarter : MonoBehaviour {

	public float respawnTime = 2.0f;
	public GameObject player;
	public Transform spawnPoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	IEnumerator Restart(){
		yield return new WaitForSeconds(respawnTime);

		GameObject newPlayer = Instantiate (player, spawnPoint.position, Quaternion.Euler (new Vector3 (0, 0, 0)));
		gameObject.GetComponent<UnityStandardAssets._2D.Camera2DFollow>().target = newPlayer.transform.GetChild(0);
	}
}
