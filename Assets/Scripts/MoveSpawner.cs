using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpawner : MonoBehaviour {
	public float x_disp;
	public float y_disp;

	float x_move;
	float y_move;

	private Vector3 start_pos;
	private Vector3 end_pos;

	public float speed;

	void Start(){
		start_pos = transform.position;
		end_pos = new Vector3 (start_pos.x + x_disp, start_pos.y + y_disp, start_pos.z);
	}

	void Update(){
		if (transform.position.x <= start_pos.x) {
			x_move = Time.deltaTime * speed;
		} else if (transform.position.x >= end_pos.x) {
			x_move = Time.deltaTime * -speed;
		}

		if (transform.position.y <= start_pos.y) {
			y_move = Time.deltaTime * speed;
		} else if (transform.position.y >= end_pos.y) {
			y_move = Time.deltaTime * -speed;
		}

		transform.position = new Vector3 (transform.position.x + x_move, transform.position.y + y_move, transform.position.z);
	}

}
