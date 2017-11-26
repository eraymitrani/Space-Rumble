using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InControl;


public class GameLoopManager : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {

		if (InputManager.ActiveDevice.GetControl(InputControlType.Back)){
            Time.timeScale = Time.timeScale == 1 ? 0 : 1;
		}

		if (InputManager.ActiveDevice.GetControl(InputControlType.Start)) {
			SceneManager.LoadScene ("MainMenu");
		}

	}

}
