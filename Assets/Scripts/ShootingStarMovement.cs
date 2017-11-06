using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStarMovement : MonoBehaviour {

	public int starSpeed = 2;

	private Rigidbody2D rb;
	private BoxCollider2D bc;
	private PhysicsMaterial2D pm;

	private int bounces = 0;
	private Vector2 movedir = Vector2.right;

	void Start () {
		// Get Components
		rb = this.GetComponent<Rigidbody2D> ();
		bc = this.GetComponent<BoxCollider2D> ();

		// Create Physics Material to control star's bounce
		pm = new PhysicsMaterial2D ();
		//pm.bounciness = 1.0f;
		pm.bounciness = 0.75f;
		pm.friction = 0f;
		bc.sharedMaterial = pm;

		// Check movement direction
		if (this.transform.position.x > 0) {
			movedir = Vector2.left;
		}

		rb.velocity = (movedir + Vector2.down) * starSpeed;

		StartCoroutine (Falling ());
	}

	void Update() {
		// Catch the star if it goes off screen
		if (Mathf.Abs (this.transform.position.x) > 20 || Mathf.Abs (this.transform.position.y) > 20) {
			Destroy (this.gameObject);
		}
	}

	IEnumerator Falling() {

		while (true) {

			// Bounce when we hit the ground
//			if (IsGrounded ()) {
//				StartCoroutine (Bouncing ());
//				yield break;
//			}

			// Keep velocity roaring
			//rb.velocity = (movedir + Vector2.down) * starSpeed;

			yield return null;
		}
	}

	IEnumerator Bouncing() {

		while (true) {

			// Bounce Again
			if (IsGrounded()) {
				bounces++;
				pm.bounciness *= 0.90f;
				bc.sharedMaterial = pm;

				// Stop Bouncing
				if (bounces >= 3) {
					Destroy (this.gameObject);
				}
			}

			yield return null;
		}
	}

	bool IsGrounded() {
		//return Physics2D.Raycast(this.transform.position, Vector2.down, 0.5f);
		return false;
	}

}
