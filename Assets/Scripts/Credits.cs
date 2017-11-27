using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class Credits : MonoBehaviour {

    public GameObject mainMenuPanel;


	// Update is called once per frame
	void Update ()
    {
        if (InputManager.ActiveDevice.Action1.WasPressed ||
            InputManager.ActiveDevice.Action2.WasPressed ||
            InputManager.ActiveDevice.Action3.WasPressed ||
            InputManager.ActiveDevice.Action4.WasPressed ||
            InputManager.ActiveDevice.GetControl(InputControlType.Start) ||
            InputManager.ActiveDevice.GetControl(InputControlType.Select))
        {
            mainMenuPanel.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
