using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public Vector3 spawnValues;
	public float spawnWait = 1.0f;
	public int hazardCount = 10;
	public float startWait = 1.0f;
	public float waveWait = 5.0f;
	public Text scoreText;
	public Text gameOverText;
	public Text restartText;

	private int score;
	private bool gameOver, restart; 

	// Use this for initialization
	void Start () {
		score = 0;
		gameOver = false; 
		restart = false;
		gameOverText.text = "";
		restartText.text = "";
		UpdateScore ();
		StartCoroutine (spawnWaves ());
	}

	void Update(){
		if (restart) {
			if(Input.GetKeyDown(KeyCode.R)){
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}
	}

	IEnumerator spawnWaves(){
		yield return new WaitForSeconds (startWait);
		while (true) {
			for (int i = 0; i < hazardCount; i++) {
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y ,spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity; //No rotation
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (gameOver) {
				restartText.text= "Press R for Restart";
				restart = true;
				break;
			}
		}
	}

	public void AddScore (int newScoreValue){
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore(){
		scoreText.text = "Score : " + score;
	}

	public void GameOverUpdate(){
		gameOverText.text = "Game Over!!";
		gameOver = true;
	}
}


