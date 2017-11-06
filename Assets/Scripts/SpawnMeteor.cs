using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeteor : MonoBehaviour {

	public GameObject meteor;

	public int spawnWait = 15;

	void Start () {
		StartCoroutine (SpawnLoop ());
	}

	IEnumerator SpawnLoop() {
		// change to "while !gameOver
		while (true) {

			GameObject m = GameObject.Instantiate (meteor);

			if (Random.value < 0.5) {
				m.transform.position = new Vector2 (-10, 10);
			}
			else {
				m.transform.position = new Vector2 (10, 10);
			}

			// Wait for next spawn
			yield return new WaitForSeconds (spawnWait);
		}
	}
}
