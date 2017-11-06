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

		//meteorcrash += Explode;

		if (this.transform.position.x > 0) {
			movedir = Vector2.left + Vector2.down;
			this.transform.localScale = new Vector2 (-3, 3);
		}
		else {
			movedir = Vector2.right + Vector2.down;
		}
	}
	
	void Update () {
		rb.velocity = movedir * meteorSpeed;
	}

	void OnCollisionEnter2D(Collision2D other) {
//		if (other.collider.tag == "ground") {
//			//Explode ();
//		}
		Explode();
	}

	void Explode() {
		Debug.Log (meteorcrash);
		Destroy (this.gameObject);
		//return null;
	}
}
