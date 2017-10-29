using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafMovement : MonoBehaviour {

	private Collider2D col;
	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		col = this.GetComponent<Collider2D> ();
		rb = this.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		rb.velocity = new Vector2 (Mathf.Cos (Time.time) * 4f, -4.9f);
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
