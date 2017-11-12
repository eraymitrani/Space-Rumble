using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1) Set Bounds for what the Camera will Focus on
// 2) Average all player locations with the focal point of the scene
// 3) Lerp the camera toward that new average

public class CameraScroller : MonoBehaviour {

	// Outer Limits past which the camera does not care about the players
	public float xBounds = 20f;
	public float yBounds = 15f;

	// Speed at which camera lerps
	public float updateSpeed = 5f;

	// Coordinates of the Camera's center for the level
	public Vector2 focalPoint;

	private Bounds bounds;
	private GameObject[] players;


	void Start () {
		// Find all players
		players = GameObject.FindGameObjectsWithTag ("Player");

		// Set the limits of what the camera cares about
		bounds = new Bounds();
		bounds.Encapsulate(new Vector3(focalPoint.x - xBounds, focalPoint.y - yBounds, 0));
		bounds.Encapsulate(new Vector3(focalPoint.x + xBounds, focalPoint.y + yBounds, 0));
	}

	private void LateUpdate () {
		Vector2 pos = averagePositions();

		// Lerp toward new values
		float x = Mathf.MoveTowards(this.transform.position.x, pos.x, updateSpeed * Time.deltaTime);
		float y = Mathf.MoveTowards(this.transform.position.y, pos.y, updateSpeed * Time.deltaTime);
		this.transform.position = new Vector3 (x, y, -10);
	}
		
	private Vector2 averagePositions() {

		// Average player positions
		Vector2 total = Vector2.zero;
		for (int i = 0; i < players.Length; i++) {
			Vector2 playerPosition = players[i].transform.position;

			// Gets clamp of out-of-bounds player
			if (!bounds.Contains(playerPosition)) {
				float playerX = Mathf.Clamp(playerPosition.x, bounds.min.x, bounds.max.x);
				float playerY = Mathf.Clamp(playerPosition.y, bounds.min.y, bounds.max.y);
				playerPosition = new Vector2(playerX, playerY);
			}

			// Sums all positions
			total += playerPosition;
		}

		// Include the level focus
		total += focalPoint;
		return total / (players.Length + 1);
	}
}