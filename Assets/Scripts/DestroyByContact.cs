using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion, playerExplosion;
	public int scoreValue;
	private GameController gameController;

	void Start(){
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find game controller script");
		}
	}

	void OnTriggerEnter(Collider other){
		
		if (other.CompareTag("Boundary") || other.CompareTag("Enemy")) {
			return;
		}

		if (other.CompareTag("Player")) {
			Instantiate (playerExplosion, other.GetComponent<Transform>().position, other.GetComponent<Transform>().rotation);
			gameController.GameOverUpdate ();
			CameraShake.shakeDuration = 0.5f;
		}

		if (explosion != null) {
			Instantiate (explosion, gameObject.GetComponent<Transform>().position, gameObject.GetComponent<Transform>().rotation);
		}

		gameController.AddScore (scoreValue);
		Destroy (other.gameObject);
		Destroy (gameObject);

	}
}
