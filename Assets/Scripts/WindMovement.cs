using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMovement : MonoBehaviour {

	public int windForce;

	private Rigidbody2D rb;
	private bool right = true;

	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();

		// East or West wind?
		if (this.transform.position.x > 0) {
			right = false;
			this.transform.localScale = new Vector2 (-1, 1);
		}

		StartCoroutine (Form ());
	}

	IEnumerator Form() {

		// Do nothing but stay in place for now
		// Will extend later
		yield return new WaitForSeconds(1);

		StartCoroutine (Move ());

	}
	
	IEnumerator Move () {

		while (true) {
			// Maintain a constant speed
			if (right) {
				rb.velocity = Vector2.right * windForce;
			}
			else {
				rb.velocity = Vector2.left * windForce;
			}

			yield return null;
		}
	}
}
