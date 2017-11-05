using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorMovement : MonoBehaviour {

	public int meteorSpeed = 2;

	private Rigidbody2D rb;

	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
	}
	
	void Update () {
		rb.velocity = (Vector2.right + Vector2.down) * meteorSpeed;
	}

	void OnCollisionEnter(Collision other) {
		Debug.Log ("Colliding!");
		if (other.collider.tag == "ground") {
			Destroy (this.gameObject);
		}
	}
}
