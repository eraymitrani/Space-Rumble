using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerMovement : MonoBehaviour {

	Rigidbody rb;
	public GameObject maincam;
	public float move_speed;
	public float jump_force;
	public bool can_jump;

	Vector3 input_vec;
	int should_jump; //0 is nothing, 1 is going up, 2 is going down

	int press_times;

	// Use this for initialization
	void Start () {
		Cursor.visible = false;

		rb = GetComponent<Rigidbody> ();
		rb.velocity = Vector3.zero;
		can_jump = true;
		press_times = 0;
	}

	// Update is called once per frame
	void Update () {

		getInput ();

		if ((InputManager.ActiveDevice.Action1.WasPressed || Input.GetKeyDown(KeyCode.Space)) && can_jump) {
			should_jump = 1;	//go up
		} else if (InputManager.ActiveDevice.Action1.WasReleased || Input.GetKeyUp(KeyCode.Space)) {
			should_jump = 2; 	//go down
		} else {
			should_jump = 0;
		}

	}
	void LateUpdate(){
		doInput ();
		doJumpInput (should_jump);
	}

	void getInput(){
		Vector3 return_vec;

		if (Input.GetAxisRaw ("Horizontal") > 0) {
			input_vec = new Vector3 (move_speed * Input.GetAxisRaw("Horizontal"), rb.velocity.y, 0);
		} else if (Input.GetAxisRaw ("Horizontal") < 0) {
			input_vec = new Vector3 (move_speed * Input.GetAxisRaw("Horizontal"), rb.velocity.y, 0);
		} else {
			input_vec = new Vector3 (0, rb.velocity.y, 0);
		}


		RaycastHit hit;
		float half_height = GetComponent<SpriteRenderer> ().bounds.size.y / 2 + 0.01f;
		float half_width = GetComponent<SpriteRenderer> ().bounds.size.x / 2 + 0.01f;

		//0.08f --> Time.fixedDeltaTime * rb.velocity.magnitude ? 
		if ((rb.SweepTest (Vector3.left, out hit, 0.08f) && Input.GetAxisRaw("Horizontal") < 0) || 
			(rb.SweepTest (Vector3.right, out hit, 0.08f) && Input.GetAxisRaw("Horizontal") > 0)) {
			
			if (hit.collider.gameObject.tag == "ground" || hit.collider.gameObject.tag == "wall") {
				input_vec = new Vector3 (0, rb.velocity.y, 0);
			}
		}

		if (Physics.Raycast (transform.position, Vector3.down, out hit, half_height) /*&& !can_jump*/) {
			if (hit.collider.gameObject.tag == "ground" || hit.collider.gameObject.tag == "wall" || hit.collider.gameObject.tag == "coyote") {
				can_jump = true;
			}
		} else {
			//can_jump = false;
		}
	}

	void doInput(){
		rb.velocity = input_vec;
	}
	void doJumpInput(int jump_state){
		if (jump_state == 0) {
			return;
		} else if (jump_state == 1) {
			can_jump = false;
			//Debug.Log(rb.velocity.y);
			rb.velocity = Vector3.zero;
			rb.AddForce (new Vector3 (0, jump_force, 0), ForceMode.VelocityChange);
		} else if (jump_state == 2) {
			if (rb.velocity.y > 0) {
				rb.velocity = new Vector3 (rb.velocity.x, 0.2f, 0);
			}
		}
	}

	void OnCollisionEnter(Collision coll){
		if (coll.collider.tag == "wall") {
			rb.velocity = Vector3.zero;
		}
	}
}
