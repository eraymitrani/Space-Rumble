using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafMovement : MonoBehaviour {

	private Collider2D col;
	private Rigidbody2D rb;

	void Start () {
		col = this.GetComponent<Collider2D> ();
		rb = this.GetComponent<Rigidbody2D> ();
	}
	
	void Update () {
		// Sway in the air
		if (!IsGrounded()) {
			rb.velocity = new Vector2 (Mathf.Sin (Time.time) * 2f, 0);
		} 
		// Rest on the Ground
		else {
			rb.velocity = Vector2.zero;
		}
	}
		
	public bool IsGrounded() {
		Ray ray = new Ray(col.bounds.center, Vector2.down);
		float radius = col.bounds.extents.x - .05f;
		float fullDistance = col.bounds.extents.y + .05f;

		if (Physics.SphereCast(ray, radius, fullDistance)) {
			return true;
		}
		else {
			return false;
		}
	}

}
