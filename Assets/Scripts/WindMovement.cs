using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMovement : MonoBehaviour {

	public int windForce;

	private Rigidbody2D rb;
	private bool right = true;

	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
	}
	
	void Update () {
		// Maintain a constant speed
		if (right) {
			rb.velocity = Vector2.right * windForce;
		}
		else {
			rb.velocity = Vector2.left * windForce;
		}
	}
}
