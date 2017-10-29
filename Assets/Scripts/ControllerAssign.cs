using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.SceneManagement;

public class ControllerAssign : MonoBehaviour {

    public GameObject Player1Join;
    public GameObject Player1Joined;
    public GameObject Player2Join;
    public GameObject Player2Joined;
    public GameObject Player3Join;
    public GameObject Player3Joined;
    public GameObject Player4Join;
    public GameObject Player4Joined;
    public GameObject TrackSelect;

    bool p1 = false, p2 = false, p3 = false, p4 = false;
    HashSet<InputDevice> usedControllers = new HashSet<InputDevice>();

    void Update()
    {
        if(InputManager.ActiveDevice.Action1.WasPressed)
        {
            if (usedControllers.Contains(InputManager.ActiveDevice))
                return;

            if (!p1)
            {
                PlayerControllers.Player1 = InputManager.ActiveDevice;
                p1 = true;
                Player1Join.SetActive(false);
                Player1Joined.SetActive(true);
            }
            else if(!p2)
            {
                PlayerControllers.Player2 = InputManager.ActiveDevice;
                p2 = true;
                Player2Join.SetActive(false);
                Player2Joined.SetActive(true);
            }
            else if (!p3)
            {
                PlayerControllers.Player3 = InputManager.ActiveDevice;
                p3 = true;
                Player3Join.SetActive(false);
                Player3Joined.SetActive(true);
            }
            else if (!p4)
            {
                PlayerControllers.Player4 = InputManager.ActiveDevice;
                p4 = true;
                Player4Join.SetActive(false);
                Player4Joined.SetActive(true);
            }

            usedControllers.Add(InputManager.ActiveDevice);
        }
        if (PlayerControllers.Player1 != null && InputManager.ActiveDevice.GetControl(InputControlType.Start))
        {
            TrackSelect.SetActive(true);
            gameObject.SetActive(false);
        }

    }
}
