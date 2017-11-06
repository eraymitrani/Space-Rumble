using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorMovement : MonoBehaviour {

	public delegate IEnumerator MeteorCrash();
	public static MeteorCrash meteorcrash;
    public Vector2 movedir;

    public int meteorSpeed = 2;

	private Rigidbody2D rb;

	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();

		meteorcrash += Explode;
	}
	
	void Update () {
		rb.velocity = movedir * meteorSpeed;
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
