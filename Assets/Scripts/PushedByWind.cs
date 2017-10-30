using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushedByWind : MonoBehaviour {

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
	}
	
	void OnCollision(Collision other) {
		Debug.Log ("Colliding!");
//		if (other.tag == "wind") {
//			rb.velocity = other.GetComponent<Rigidbody2D> ().velocity;
//		}
	}
}
