using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroller : MonoBehaviour {

	public int stageRightBoundary = 10;
	public int stageLeftBoundary = -10;
	public int stageTopBoundary = 10;
	public int stageBottomBoundary = 0;

	private GameObject[] players;

	void Start () {
		players = GameObject.FindGameObjectsWithTag ("Player");
	}
		
	void Update () {

		// Find the most extreme x and y coordinates of each player
		float minX = 0;
		float minY = 0;
		float maxX = 0;
		float maxY = 0;

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

	}
}
