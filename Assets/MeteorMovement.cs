using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorMovement : MonoBehaviour {

	public delegate IEnumerator MeteorCrash();
	public static MeteorCrash meteorcrash;

	public int meteorSpeed = 2;

	private Rigidbody2D rb;
	private Vector2 movedir;

	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();

		if (this.transform.position.x > 0) {
			movedir = Vector2.left;
			this.transform.localScale = new Vector2 (-3, 3);
		} 
		else {
			movedir = Vector2.right;
		}

		meteorcrash += Explode;
	}
	
	void Update () {
		rb.velocity = (movedir + Vector2.down) * meteorSpeed;
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.collider.tag == "ground") {
			meteorcrash ();
		}
	}

	IEnumerator Explode() {
		Debug.Log (meteorcrash);
		Destroy (this.gameObject);
		return null;
	}
}
