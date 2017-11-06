using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovement : MonoBehaviour {

	float move_speed = 0.005f;
	float timer, wait_time;


	// Use this for initialization
	void Start () {
		timer = 0;
		wait_time = 5;
	}
	
	// Update is called once per frame
	void Update () {
		if (timer < wait_time) {
			timer += Time.deltaTime;
			return;
		}

		if (transform.position.x < 0) {
			transform.position = new Vector3 (transform.position.x + move_speed, transform.position.y, transform.position.z);
		} else {
			transform.position = new Vector3 (transform.position.x - move_speed, transform.position.y, transform.position.z);
		}
	}
}
