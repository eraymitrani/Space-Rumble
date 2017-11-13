using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShootingStars : MonoBehaviour {

	public GameObject shootingStar;

	public int spawnWait = 3;

	int leftBoundary = -10;
	int rightBoundary = 10;
	int downBoundary = 5;
	int upBoundary = 10;

	void Start () {
		StartCoroutine (SpawnLoop ());
	}

	IEnumerator SpawnLoop() {
        // change to "while !gameOver
        yield return new WaitForSeconds(spawnWait);
		while (true) {

			float xcoord = Random.Range (leftBoundary, rightBoundary);
			float ycoord = Random.Range (downBoundary, upBoundary);

			GameObject ss = GameObject.Instantiate (shootingStar);
			ss.transform.position = new Vector2 (xcoord, ycoord);

			// Wait for next spawn
			yield return new WaitForSeconds (spawnWait);
		}
	}
}

