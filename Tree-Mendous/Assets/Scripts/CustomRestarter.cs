using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRestarter : MonoBehaviour {

	public float respawnTime = 2.0f;
	public GameObject player;
	public Transform spawnPoint;
    public PlayerController playerController;

    bool soundPlayed = false;

    // Use this for initialization
    void Start () {
        playerController = player.GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(Restart());
        }
    }



    IEnumerator Restart(){

        player.SetActive(false);
        GameObject.FindWithTag("MainCamera").GetComponent<AudioSource>().PlayOneShot(playerController.deathSound, playerController.deathVolume);
        yield return new WaitForSeconds(respawnTime);       
        playerController.currentHealth = playerController.maxHealth;
        playerController.currentPower = 0;
        playerController.heart1.gameObject.SetActive(true);
        playerController.heart2.gameObject.SetActive(true);
        playerController.heart3.gameObject.SetActive(true);
        playerController.heart4.gameObject.SetActive(true);
        playerController.energy1.gameObject.SetActive(false);
        playerController.energy2.gameObject.SetActive(false);
        playerController.energy3.gameObject.SetActive(false);
        playerController.energy4.gameObject.SetActive(false);
        playerController.energy5.gameObject.SetActive(false);
        player.SetActive(true);
        player.transform.position = spawnPoint.position;  
        
		//GameObject newPlayer = Instantiate (player, spawnPoint.position, Quaternion.Euler (new Vector3 (0, 0, 0)));
        //gameObject.GetComponent<UnityStandardAssets._2D.Camera2DFollow>().target = newPlayer.transform.GetChild(0);
	}
}
