using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleVortex : MonoBehaviour {

	public int gravitySlowForce = 4;

	public float gravityZone = 0.25f;

	void OnTriggerStay2D(Collider2D other) {

		// Disable all scripts in the target object
		// This is what allows us to overwrite the movement
		if (other.gameObject.tag != "Player") {
			MonoBehaviour[] scripts = other.gameObject.GetComponentsInChildren<MonoBehaviour> ();
			for (int i = 0; i < scripts.Length; ++i) {
				scripts [i].enabled = false;
			}
		}
			
		Rigidbody2D a = this.GetComponent<Rigidbody2D> ();
		Rigidbody2D b = other.gameObject.GetComponent<Rigidbody2D> ();

		// F = G * m1 * m2 / distance^2
		// Eliminated G which makes all the physics unworkable
		// Only apply gravity when outside a certain range or else it wildly oscillates
		Vector2 dist = a.position - b.position;
		float force = 0;
		if (dist.magnitude > gravityZone) {
			force = a.mass * b.mass / dist.sqrMagnitude / gravitySlowForce;
		}
		b.velocity = dist * force;
	}
}
