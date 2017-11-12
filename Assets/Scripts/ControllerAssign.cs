using System;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerAssign : MonoBehaviour {

    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;
    public GameObject TrackSelect;
    public float deadZone = 0.5f;
    public Color[] PlayerColors;
    public bool allowSinglePlayer = false;
    public bool skipSceneSelect = true;

    bool p1 = false, p2 = false, p3 = false, p4 = false;
    HashSet<InputDevice> usedControllers = new HashSet<InputDevice>();
    GameObject p1Color, p1Char, p2Color, p2Char, p3Color, p3Char, p4Color, p4Char;
    int[] pColorI = new int[] { -1, -1, -1, -1 };
    bool p1Moved = false, p2Moved = false, p3Moved = false, p4Moved = false;

    void Awake()
    {
        PlayerControllers.reset();
        p1Color = Player1.transform.Find("Joined/Panel/ColorPanel").gameObject;
        p2Color = Player2.transform.Find("Joined/Panel/ColorPanel").gameObject;
        p3Color = Player3.transform.Find("Joined/Panel/ColorPanel").gameObject;
        p4Color = Player4.transform.Find("Joined/Panel/ColorPanel").gameObject;

        p1Char = Player1.transform.Find("Joined/Panel/PlayerPreview").gameObject;
        p2Char = Player2.transform.Find("Joined/Panel/PlayerPreview").gameObject;
        p3Char = Player3.transform.Find("Joined/Panel/PlayerPreview").gameObject;
        p4Char = Player4.transform.Find("Joined/Panel/PlayerPreview").gameObject;
    }

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
                Player1.transform.Find("Join").gameObject.SetActive(false);
                Player1.transform.Find("Joined").gameObject.SetActive(true);
                int newColor = 0;
                while (Array.Exists<int>(pColorI, i => i == newColor))
                {
                    newColor = (newColor + 1) % PlayerColors.Length;
                }
                pColorI[0] = newColor;
                UpdateColor(p1Color, p1Char, pColorI[0], 1);
            }
            else if(!p2)
            {
                PlayerControllers.Player2 = InputManager.ActiveDevice;
                p2 = true;
                Player2.transform.Find("Join").gameObject.SetActive(false);
                Player2.transform.Find("Joined").gameObject.SetActive(true);
                int newColor = 0;
                while (Array.Exists<int>(pColorI, i => i == newColor))
                {
                    newColor = (newColor + 1) % PlayerColors.Length;
                }
                pColorI[1] = newColor;
                UpdateColor(p2Color, p2Char, pColorI[1], 2);
            }
            else if (!p3)
            {
                PlayerControllers.Player3 = InputManager.ActiveDevice;
                p3 = true;
                Player3.transform.Find("Join").gameObject.SetActive(false);
                Player3.transform.Find("Joined").gameObject.SetActive(true);
                int newColor = 0;
                while (Array.Exists<int>(pColorI, i => i == newColor))
                {
                    newColor = (newColor + 1) % PlayerColors.Length;
                }
                pColorI[2] = newColor;
                UpdateColor(p3Color, p3Char, pColorI[2], 3);
            }
            else if (!p4)
            {
                PlayerControllers.Player4 = InputManager.ActiveDevice;
                p4 = true;
                Player4.transform.Find("Join").gameObject.SetActive(false);
                Player4.transform.Find("Joined").gameObject.SetActive(true);
                int newColor = 0;
                while (Array.Exists<int>(pColorI, i => i == newColor))
                {
                    newColor = (newColor + 1) % PlayerColors.Length;
                }
                pColorI[3] = newColor;
                UpdateColor(p4Color, p4Char, pColorI[3], 4);
            }

            usedControllers.Add(InputManager.ActiveDevice);
        }

        if (PlayerControllers.Player1 != null && (PlayerControllers.Player2 != null || allowSinglePlayer) && InputManager.ActiveDevice.GetControl(InputControlType.Start))
        {
            if(skipSceneSelect)
            {
                SceneManager.LoadScene(1);
                return;
            }
            TrackSelect.SetActive(true);
            gameObject.SetActive(false);
        }

        PlayerColorCheck(PlayerControllers.Player1, ref p1Moved, ref pColorI[0], p1Color, p1Char);
        PlayerColorCheck(PlayerControllers.Player2, ref p2Moved, ref pColorI[1], p2Color, p2Char);
        PlayerColorCheck(PlayerControllers.Player3, ref p3Moved, ref pColorI[2], p3Color, p3Char);
        PlayerColorCheck(PlayerControllers.Player4, ref p4Moved, ref pColorI[3], p4Color, p4Char);
    }

    void UpdateColor(GameObject panel, GameObject player, int index, int playerNum)
    {
        panel.GetComponent<Image>().color = PlayerColors[index];
        player.GetComponent<Image>().color = PlayerColors[index];
        switch(playerNum)
        {
            case 1: PlayerControllers.Color1 = PlayerColors[index]; break;
            case 2: PlayerControllers.Color2 = PlayerColors[index]; break;
            case 3: PlayerControllers.Color3 = PlayerColors[index]; break;
            case 4: PlayerControllers.Color4 = PlayerColors[index]; break;
        }
    }

    void PlayerColorCheck(InputDevice controller, ref bool pMoved, ref int pColorInd, GameObject pColor, GameObject pChar)
    {
        if (controller != null)
        {
            if (controller.LeftStickX > deadZone && !pMoved)
            {
                int newColor = (pColorInd + 1) % PlayerColors.Length;
                while(Array.Exists<int>(pColorI, i => i == newColor))
                {
                    newColor = (newColor + 1) % PlayerColors.Length;
                }
                pColorInd = newColor;
                UpdateColor(pColor, pChar, pColorInd, 1);
                pMoved = true;
            }
            else if (controller.LeftStickX < -deadZone && !pMoved)
            {
                int newColor = (pColorInd - 1) % PlayerColors.Length;
                while (Array.Exists<int>(pColorI, i => i == newColor))
                {
                    newColor = (newColor - 1) % PlayerColors.Length;
                }
                pColorInd = newColor;
                UpdateColor(pColor, pChar, pColorInd, 1);
                pMoved = true;
            }
            else if (Mathf.Abs(controller.LeftStickX) < deadZone)
            {
                pMoved = false;
            }
        }
    }
}
