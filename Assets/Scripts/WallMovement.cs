using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovement : MonoBehaviour {

	float move_speed = 0.005f;
	float timer, wait_time, end_time;
	public bool is_top;


	// Use this for initialization
	void Start () {
		timer = 0;
		wait_time = 10;
		end_time = 25;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer < wait_time) {
			return;
		}

		if (timer > end_time) {
			return;
		}

		if (is_top) {
			transform.position = new Vector3 (transform.position.x, transform.position.y - move_speed, transform.position.z);
		} else {


			if (transform.position.x < 0) {
				transform.position = new Vector3 (transform.position.x + move_speed, transform.position.y, transform.position.z);
			} else {
				transform.position = new Vector3 (transform.position.x - move_speed, transform.position.y, transform.position.z);
			}
		}


	}
}
