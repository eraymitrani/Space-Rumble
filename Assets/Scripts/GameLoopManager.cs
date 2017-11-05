using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InControl;
using UnityEngine.UI;


public class GameLoopManager : MonoBehaviour {

    ScoreManager scoreManager;

	// Use this for initialization
	void Start () {
        scoreManager = GetComponent<ScoreManager>();
	}
	
	// Update is called once per frame
	void Update () {

		if (InputManager.ActiveDevice.GetControl(InputControlType.Back)){
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}

		if (InputManager.ActiveDevice.GetControl(InputControlType.Start)) {
			SceneManager.LoadScene ("MainMenu");
		}
	}
}
