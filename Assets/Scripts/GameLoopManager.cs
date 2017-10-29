using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InControl;


public class GameLoopManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

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
