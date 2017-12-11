using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindLifetime : MonoBehaviour {

	float lifetime;
	float timer = 0;
	public bool is_alive = false;

	// Use this for initialization
	void Start () {
		lifetime = Random.Range (0.25f, 0.35f);
		//Destroy (this.gameObject, 2);
	}
	
	// Update is called once per frame
	void Update () {
		if (is_alive) {
			timer += Time.deltaTime;
			if (timer >= lifetime) {
				Destroy (gameObject);
			}
		}
	}


	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "wind pixel") {
			Debug.Log ("colliding");

		}
		if (other.gameObject.tag == "wall" || other.gameObject.tag == "crate" || other.gameObject.tag == "mid_wall") {
			Destroy (this.gameObject);
		}
	}
}
