using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroller : MonoBehaviour {

	public float min_width = 5;
	public float min_height = 5;
	public float max_width = 10;
	public float max_height = 10;

	public float buffer = 2;

	private GameObject[] players;

	void Start () {
		players = GameObject.FindGameObjectsWithTag ("space rock");
	}
		
	void Update () {

		// Find the most extreme x and y coordinates of each player
		float minX = 0f;
		float minY = 0f;
		float maxX = 0f;
		float maxY = 0f;

		for (int i = 0; i < players.Length; ++i) {
			// x coord
			if (players [i].transform.position.x > maxX) {
				maxX = players [i].transform.position.x;
			} 
			else if (players [i].transform.position.x < minX) {
				minX = players [i].transform.position.x;
			}

			// y coord
			if (players [i].transform.position.y > maxY) {
				maxY = players [i].transform.position.y;
			} 
			else if (players [i].transform.position.y < minY) {
				minY = players [i].transform.position.y;
			}
		}

		float width = maxX - minX;
		float height = maxY - minY;

		// Find the middle of the max view, the current view, and the min view




		Camera.main.rect = new Rect (Camera.main.rect.x, Camera.main.rect.y, width + buffer, height + buffer);
	}
}
