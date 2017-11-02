using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFire : MonoBehaviour {

	public GameObject Fire;

	public int fire_wait_time = 10;

	private float groundCoords = -2.42f;

	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnLoop ());
	}

	IEnumerator SpawnLoop() {
		// This should be changed to only be true when game is active
		while (true) {

			// Spawn Fire
			float xcoord = Random.Range (-10, 10);
			GameObject f = GameObject.Instantiate (Fire);

			f.transform.position = new Vector2 (xcoord, groundCoords);

			// Wait for next spawn
			yield return new WaitForSeconds (fire_wait_time);

			// Let the Fire burn out
			Destroy (f);
		}
	}
}
