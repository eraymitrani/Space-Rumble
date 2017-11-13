using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleCenter : MonoBehaviour {

	public int shredTime = 6;
	public float shredForce = 0.95f;

	void OnTriggerEnter2D(Collider2D other) {
		StartCoroutine (Shred (other));
	}

	IEnumerator Shred(Collider2D other) {

		// Shrink the object
		for (float t = 0; t < shredTime; t += Time.fixedDeltaTime) {
			other.transform.localScale *= shredForce;
			yield return null;
		}

		yield return null;

		// Kill the object
		if (other.gameObject.tag != "Player") {
			Destroy (other.gameObject);
		}
		else {
			Inventory inv = other.gameObject.GetComponent<Inventory> ();
			inv.Damage (1);
		}
	}
}
