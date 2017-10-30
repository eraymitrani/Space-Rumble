using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InControl;
using UnityEngine.UI;


public class GameLoopManager : MonoBehaviour {

	public float game_timer;
	public Text time_left;

	// Use this for initialization
	void Start () {
		game_timer = 30;
	}
	
	// Update is called once per frame
	void Update () {

		game_timer -= Time.deltaTime;
		time_left.text = Mathf.Floor (game_timer).ToString();
		if (game_timer <= 0.2) {
			SceneManager.LoadScene ("MainMenu");
		}

		if (InputManager.ActiveDevice.GetControl(InputControlType.Back)){
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}

		if (InputManager.ActiveDevice.GetControl(InputControlType.Start)) {
			SceneManager.LoadScene ("MainMenu");
		}
	}
}
