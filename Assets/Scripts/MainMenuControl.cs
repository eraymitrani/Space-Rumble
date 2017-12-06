using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class MainMenuControl : MonoBehaviour {

    public GameObject selector;
    public GameObject playerPanel;
    public GameObject creditPanel;

    public GameObject PlayButton;
    public GameObject OptionButton;

    public int totalOptions = 2;

    int index = 0;
    bool moved = false;
    float buttonSpacing = 0;

    void Start()
    {
        TotalPlayerStocks.reset();
        buttonSpacing = PlayButton.transform.position.y - OptionButton.transform.position.y;
    }

    void Update ()
    {
		if(InputManager.ActiveDevice.Action1.WasPressed ||
            InputManager.ActiveDevice.Action2.WasPressed ||
            InputManager.ActiveDevice.Action3.WasPressed ||
            InputManager.ActiveDevice.Action4.WasPressed ||
            InputManager.ActiveDevice.GetControl(InputControlType.Start) ||
            InputManager.ActiveDevice.GetControl(InputControlType.Select))
        {
            switch(index)
            {
                case 0:
                    playerPanel.SetActive(true);
                    gameObject.SetActive(false);
                    break;
                case 1:
                    creditPanel.SetActive(true);
                    gameObject.SetActive(false);
                    break;
            }
        }

        if (Mathf.Abs(InputManager.ActiveDevice.LeftStickY) < 0.01f)
            moved = false;

        if(Mathf.Abs(InputManager.ActiveDevice.LeftStickY) > 0.01f && !moved)
        {
            if(InputManager.ActiveDevice.LeftStickY < 0)
            {
                if(index != totalOptions - 1)
                {
                    index += 1;
                    selector.transform.position = selector.transform.position + new Vector3(0, -buttonSpacing, 0);
                    moved = true;
                }
            }
            else
            {
                if (index != 0)
                {
                    index -= 1;
                    selector.transform.position = selector.transform.position + new Vector3(0, buttonSpacing, 0);
                    moved = true;
                }
            }
        }
	}
}
