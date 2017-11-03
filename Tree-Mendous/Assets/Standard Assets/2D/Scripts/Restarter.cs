using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets._2D
{
    public class Restarter : MonoBehaviour
    {
		public GameObject player;
		GameObject newPlayer;
		public Transform spawnPoint;

		// For spawn delay
		public float passedWaitTime;
		public float spawnDelay;

		// Audio
		public AudioClip deathSound;
		public float deathVolume = 0.7f;
		AudioSource audioSource;

		bool startTimer = false;
		bool soundPlayed = false;

		// Use this for initialization
		void Start () {
			audioSource = GetComponent<AudioSource> ();
		}

		void FixedUpdate (){
			if (startTimer) {
				if (passedWaitTime < spawnDelay) {
					passedWaitTime = passedWaitTime + Time.deltaTime;
				} else {
					passedWaitTime = 0;
					soundPlayed = false;

					newPlayer = Instantiate (player, spawnPoint.position, Quaternion.Euler (new Vector3 (0, 0, 0)));
					GameObject mainCamera = GameObject.FindWithTag ("MainCamera");
					mainCamera.GetComponent<Camera2DFollow>().target = newPlayer.transform.GetChild(0);

					startTimer = false;
				}
			}
		}

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
				if (!soundPlayed) {
					audioSource.PlayOneShot (deathSound, deathVolume);
					soundPlayed = true;
				}

				GameObject mainCamera = GameObject.FindWithTag ("MainCamera");
				mainCamera.GetComponent<Camera2DFollow>().target = null;

                //SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
				Destroy(other.gameObject, 2f);

				startTimer = true;


            }
        }
    }
}
