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
    public Material[] PlayerMaterials = new Material[4];
    public bool allowSinglePlayer = false;
    public bool skipSceneSelect = true;

    bool[] p = new bool[4];
    bool[] pickedColor = new bool[4];
    bool[] pReady = new bool[4];
    HashSet<InputDevice> usedControllers = new HashSet<InputDevice>();
    int[] pColorI = new int[] { -1, -1, -1, -1 };
    bool[] pMoved = new bool[4];
    GameObject[] PlayerTutorial = new GameObject[4];

    void Awake()
    {
        PlayerControllers.reset();

        /*PlayerTutorial[0] = Player1.transform.Find("Ready").Find("Panel").Find("PlayerPreview").gameObject;
        PlayerTutorial[1] = Player2.transform.Find("Ready").Find("Panel").Find("PlayerPreview").gameObject;
        PlayerTutorial[2] = Player3.transform.Find("Ready").Find("Panel").Find("PlayerPreview").gameObject;
        PlayerTutorial[3] = Player4.transform.Find("Ready").Find("Panel").Find("PlayerPreview").gameObject;*/
    }

    void Update()
    {
        if(p[0] && !pickedColor[0] && PlayerControllers.Player1.Action1.WasPressed)
        {
            pickedColor[0] = true;
            Player1.transform.Find("Joined").gameObject.SetActive(false);
            Player1.transform.Find("Ready").gameObject.SetActive(true);
            return;
        }
        if (p[1] && !pickedColor[1] && PlayerControllers.Player2.Action1.WasPressed)
        {
            pickedColor[1] = true;
            Player2.transform.Find("Joined").gameObject.SetActive(false);
            Player2.transform.Find("Ready").gameObject.SetActive(true);
            return;
        }
        if (p[2] && !pickedColor[2] && PlayerControllers.Player3.Action1.WasPressed)
        {
            pickedColor[2] = true;
            Player3.transform.Find("Joined").gameObject.SetActive(false);
            Player3.transform.Find("Ready").gameObject.SetActive(true);
            return;
        }
        if (p[3] && !pickedColor[3] && PlayerControllers.Player4.Action1.WasPressed)
        {
            pickedColor[3] = true;
            Player4.transform.Find("Joined").gameObject.SetActive(false);
            Player4.transform.Find("Ready").gameObject.SetActive(true);
            return;
        }
        for(int i = 0; i < 4; i++)
        {
            if (p[i] && pickedColor[i])
            {
                /*if (PlayerTutorial[i].GetComponent<GUIPlayerControl>().hitTarget)
                {
                    PlayerTutorial[i].transform.parent.Find("Target").gameObject.SetActive(false);
                    PlayerTutorial[i].transform.parent.parent.Find("ReadyText").gameObject.SetActive(true);
                    pReady[i] = true;
                }*/
            }
        }

        if (InputManager.ActiveDevice.Action1.WasPressed)
        {
            if (usedControllers.Contains(InputManager.ActiveDevice))
                return;

            if (!p[0])
            {
                PlayerControllers.Player1 = InputManager.ActiveDevice;
                p[0] = true;
                Player1.transform.Find("Join").gameObject.SetActive(false);
                Player1.transform.Find("Joined").gameObject.SetActive(true);
                int newColor = 0;
                while (Array.Exists<int>(pColorI, i => i == newColor))
                {
                    newColor = (newColor + 1) % PlayerColors.Length;
                }
                pColorI[0] = newColor;
                UpdateColor(pColorI[0], 1);
            }
            else if(!p[1])
            {
                PlayerControllers.Player2 = InputManager.ActiveDevice;
                p[1] = true;
                Player2.transform.Find("Join").gameObject.SetActive(false);
                Player2.transform.Find("Joined").gameObject.SetActive(true);
                int newColor = 0;
                while (Array.Exists<int>(pColorI, i => i == newColor))
                {
                    newColor = (newColor + 1) % PlayerColors.Length;
                }
                pColorI[1] = newColor;
                UpdateColor(pColorI[1], 2);
            }
            else if (!p[2])
            {
                PlayerControllers.Player3 = InputManager.ActiveDevice;
                p[2] = true;
                Player3.transform.Find("Join").gameObject.SetActive(false);
                Player3.transform.Find("Joined").gameObject.SetActive(true);
                int newColor = 0;
                while (Array.Exists<int>(pColorI, i => i == newColor))
                {
                    newColor = (newColor + 1) % PlayerColors.Length;
                }
                pColorI[2] = newColor;
                UpdateColor(pColorI[2], 3);
            }
            else if (!p[3])
            {
                PlayerControllers.Player4 = InputManager.ActiveDevice;
                p[3] = true;
                Player4.transform.Find("Join").gameObject.SetActive(false);
                Player4.transform.Find("Joined").gameObject.SetActive(true);
                int newColor = 0;
                while (Array.Exists<int>(pColorI, i => i == newColor))
                {
                    newColor = (newColor + 1) % PlayerColors.Length;
                }
                pColorI[3] = newColor;
                UpdateColor(pColorI[3], 4);
            }

            usedControllers.Add(InputManager.ActiveDevice);
        }

        if (p[0] && !pickedColor[0])
        {
            PlayerColorCheck(PlayerControllers.Player1, ref pMoved[0], ref pColorI[0], 1);
        }
        if (p[1] && !pickedColor[1])
        {
            PlayerColorCheck(PlayerControllers.Player2, ref pMoved[1], ref pColorI[1], 2);
        }
        if (p[2] && !pickedColor[2])
        {
            PlayerColorCheck(PlayerControllers.Player3, ref pMoved[2], ref pColorI[2], 3);
        }
        if (p[3] && !pickedColor[3])
        {
            PlayerColorCheck(PlayerControllers.Player4, ref pMoved[3], ref pColorI[3], 4);
        }

        if (InputManager.ActiveDevice.GetControl(InputControlType.Start))
        {
            if (!allowSinglePlayer)
            {
                if (p[0] && !p[1])
                    return;
                for (int i = 0; i < 4; i++)
                {
                    if (p[i] && (!pickedColor[i] || !pReady[i]))
                        return;
                }
            }
            if (skipSceneSelect)
            {
                SceneManager.LoadScene(1);
                return;
            }
            TrackSelect.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    void UpdateColor(int index, int playerNum)
    {
        PlayerMaterials[playerNum - 1].color = PlayerColors[index];
        switch (playerNum)
        {
            case 1: PlayerControllers.Color1 = PlayerColors[index]; break;
            case 2: PlayerControllers.Color2 = PlayerColors[index]; break;
            case 3: PlayerControllers.Color3 = PlayerColors[index]; break;
            case 4: PlayerControllers.Color4 = PlayerColors[index]; break;
        }
    }

    void PlayerColorCheck(InputDevice controller, ref bool pMoved, ref int pColorInd, int playerNum)
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
                UpdateColor(pColorInd, playerNum);
                pMoved = true;
            }
            else if (controller.LeftStickX < -deadZone && !pMoved)
            {
                int newColor = (pColorInd - 1) % PlayerColors.Length;
                while (Array.Exists<int>(pColorI, i => i == newColor))
                {
                    newColor = (newColor - 1 + PlayerColors.Length) % PlayerColors.Length;
                }
                pColorInd = newColor;
                UpdateColor(pColorInd, playerNum);
                pMoved = true;
            }
            else if (Mathf.Abs(controller.LeftStickX) < deadZone)
            {
                pMoved = false;
            }
        }
    }
}
