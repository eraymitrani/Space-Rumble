using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorMovement : MonoBehaviour {

	public delegate void MeteorCrash();
	event MeteorCrash meteorcrash;


    public int meteorSpeed = 2;

	private Rigidbody2D rb;
	private Vector2 movedir;

	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();


		if (this.transform.position.x > 0) {
			movedir = Vector2.left + Vector2.down;
			this.transform.localScale = new Vector2 (-3, 3);
		}
		else {
			movedir = Vector2.right + Vector2.down;
		}

		// Explode
		meteorcrash += Explode;

		// Meteor crashing can shake camera?
		CameraShake shake = FindObjectOfType<CameraShake>();
		if (shake)
			meteorcrash += shake.Shake;
	}
	
	void Update () {
		rb.velocity = movedir * meteorSpeed;
	}

	void OnCollisionEnter2D(Collision2D other) {
//		if (other.collider.tag == "ground") {
//			//Explode ();
//		}
		meteorcrash();
	}

	void Explode() {
		Debug.Log (meteorcrash);
		Destroy (this.gameObject);
		//return null;
	}
}
